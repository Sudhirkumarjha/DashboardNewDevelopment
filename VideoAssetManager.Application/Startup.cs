using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.SqlServer;
using Serilog;
using VideoAssetManager.DataAccess;
using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using VideoAssetManager.DataAccess.DBUP;

using Microsoft.AspNetCore.Identity.UI.Services;
using VideoAssetManager.CommonUtils.Configuration;
using Newtonsoft.Json.Serialization;
using VideoAssetManager.Areas.Admin.Filters;
using VideoAssetManager.DataAccess.Common;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace VideoAssetManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<VideoAssetManagerDBContext>(options =>
            options.UseSqlServer(

            //adding wrapper service to container till scoped
            Configuration.GetConnectionString("DefaultConnection")
                                ));
            services.AddScoped<IWrapperRepository, WrapperRepository>();
            services.AddScoped<IDBUP, DBUP>();
            //       services.AddScoped<IEmailSender, EmailSender>();
            //services.AddSingleton<IEmailSender, EmailSender>();
            //services.AddSingleton<GlobalSettings>();
            services.AddSingleton<MailUtility>();
            //  services.AddScoped<IMessageProducer, RabbitMQProducer>(); //rabbitMQ
            //Adding Identiy Serivce in container
            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<VideoAssetManagerDBContext>();

            services.AddRazorPages();
            services.AddAuthorization();
            services.AddSession();

            services.AddMvc();

            services.Configure<AppConfig>(Configuration.GetSection("Configuration"));
            services.Configure<ConfigWrapper>(Configuration.GetSection("AzureKeys"));

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            services.AddControllers().AddJsonOptions(options => { });
            //services.AddMvc(options =>
            //{

            //    options.Filters.Add(typeof(CustomAuthorizationAttribute));
            //});

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;// 5000; // Limit on individual form values
                x.MultipartBodyLengthLimit = int.MaxValue; // Limit on form body size
              //  x.MultipartHeadersLengthLimit = 737280000; // Limit on form header size
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;// 837280000; // Limit on request body size
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var mediaPath = Configuration.GetValue<string>("Configuration:AppSettings:RawFootagePath");//  @"\\10.60.7.60\Media\";

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(mediaPath),
            //    RequestPath = "/wcr/contentdirectory/media",
            //    ServeUnknownFileTypes = true, // Optional: if serving types without known extensions
            //    OnPrepareResponse = ctx =>
            //    {
            //        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=3600");
            //    }
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();
            SeedDatabase(app);
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.ToString().ToLower();
                if (path.StartsWith("/identity/account/login") ||
                    path.StartsWith("/identity/account/logout") ||
                    path.StartsWith("/identity/account/register") ||
                    path.StartsWith("/api/")) // Optional: Exclude API calls
                {
                    await next();
                    return;
                }
                var signInManager = context.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();

                if (!context.User.Identity.IsAuthenticated || !signInManager.IsSignedIn(context.User))
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    logger.LogInformation("User session expired or not authenticated, redirecting to login.");
                    context.Response.Redirect("/Identity/Account/Login");
                    return;
                }

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Learner}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Content",
                    pattern: "{controller=Content}/{action=ContentDetails}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Content",
                    pattern: "{controller=Content}/{action=Create}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Create",
                    pattern: "{controller=Program}/{action=Create}/{id?}");

                endpoints.MapControllerRoute(
                   name: "CreateProgram",
                   pattern: "{controller=Program}/{action=CreateProgram}/{id?}");

                endpoints.MapControllerRoute(
                   name: "LearningIndex",
                   pattern: "{controller=Program}/{action=LearningIndex}/{id?}");

                endpoints.MapControllerRoute(
                   name: "Index",
                   pattern: "{controller=Program}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "AddQuestion",
                  pattern: "{controller=Quiz}/{action=AddQuestion}/{id?}");

            });
        }

        void SeedDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbup = scope.ServiceProvider.GetRequiredService<IDBUP>();
                dbup.Initialize();
            }
        }
    }
}
