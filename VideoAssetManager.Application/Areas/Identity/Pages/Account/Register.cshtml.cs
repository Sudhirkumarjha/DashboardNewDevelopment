using System;
using VideoAssetManager.CommonUtils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using VideoAssetManager.CommonUtils.Configuration;
using Microsoft.Extensions.Options;

namespace VideoAssetManager.Areas.Identity.Pages.Account
{
    // [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWrapperRepository _iwrapperRepository;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly AppConfig _appConfig;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IUserStore<IdentityUser> userStore,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IWrapperRepository iwrapperRepository,
            IWebHostEnvironment hostEnvironment, IOptions<AppConfig> appConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userStore = userStore;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _iwrapperRepository = iwrapperRepository;
            _hostEnvironment = hostEnvironment;
            _appConfig = appConfig.Value;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string Name { get; set; }
            [Display(Name = "Street Address")]
            public string? StreetAddress { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            [Display(Name = "Postal Code")]
            public string? PostalCode { get; set; }
            [Display(Name = "Phone Number")]
            public string? PhoneNumber { get; set; }
            public string? Role { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }
            [Display(Name = "Upload Image")]
            public string? Image { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            // Resolve the user via their email

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                userId = "00000000-0000-0000-0000-000000000000";

            ViewData["userid"] = userId;
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Input = new InputModel()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
            };
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? file, string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var userIdCheck = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                var userDetail = CreateUserDetail();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                //await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                user.Email = Input.Email;

                user.Name = Input.Name;
                user.PhoneNumber = Input.PhoneNumber;


                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (Input.Role == null)
                    {
                        await _userManager.AddToRoleAsync(user, RekhtaUtility.Role_User_Indi);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(code));

                    userDetail.Id = userId;
                    userDetail.StreetAddress = Input.StreetAddress;
                    userDetail.Email = Input.Email;
                    userDetail.Occupation = "Gov Job";
                    userDetail.MobileNumber = Input.PhoneNumber;
                    userDetail.City = Input.City;
                    userDetail.State = Input.State;
                    userDetail.PostalCode = Input.PostalCode;
                    userDetail.HostName = "rekhta";
                    userDetail.IsDeleted = false;
                    userDetail.Active = true;
                    userDetail.DOB = DateTime.ParseExact("01/01/2023", "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    userDetail.CreatedOn = DateTime.UtcNow;
                    userDetail.ModifiedOn = DateTime.UtcNow;
                    userDetail.UserType = "N";

                    _iwrapperRepository.UserDetail.Add(userDetail);
                    _iwrapperRepository.Save();


                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (file != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;

                        var tempUploads = Path.Combine(wwwRootPath, _appConfig.WCRFolderRelativePaths.WCRPath, _appConfig.WCRFolderRelativePaths.WCRTempPath);// @"wcr\temp");
                        var userUploads = Path.Combine(wwwRootPath, _appConfig.WCRFolderRelativePaths.WCRPath, _appConfig.WCRFolderRelativePaths.WCRUserInfo, _appConfig.WCRFolderRelativePaths.WCRUserImage);

                        var extension = Path.GetExtension(file.FileName);
                        var tempImage = Path.Combine(tempUploads, userId + extension);
                        var userImage = Path.Combine(userUploads, userId + extension);

                        using (var fileStreams = new FileStream(tempImage, FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }
                        if (!RekhtaUtility.MoveFile(tempImage, userImage))
                        {
                            _logger.LogDebug("User {0} images not uploaded.", userId);
                        }
                    }
                }
                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                //}
                //    else
                //    {
                //        if (User.IsInRole(RekhtaUtility.Role_Admin))
                //        {
                //            TempData["success"] = "New User Created Successfully";
                //        }
                //        else
                //        {
                //            await _signInManager.SignInAsync(user, isPersistent: false);

                //        }
                //        _logger.LogInformation("User Created Successfully ID : {0} email id : {1}", userId, user.Email);
                //        return LocalRedirect(returnUrl);
                //    }
                //}
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);

                else
                {
                    foreach (var error in result.Errors)
                    {
                        TempData["error"] = error.Description;
                    }
                    return new RedirectToPageResult("Register");
                }
            }
            if (string.IsNullOrEmpty(userIdCheck))
            {
                //TempData["success"] = "User created a new account with password.";
                return new RedirectToPageResult("Login");
            }
            else
            {
                TempData["success"] = "User created a new account with password.";
                return new RedirectToPageResult("Register");
            }
        }


        private UserMaster CreateUser()
            {
                try
                {
                    return Activator.CreateInstance<UserMaster>();
                }
                catch
                {
                    _logger.LogDebug($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                        $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                        $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");

                    throw new InvalidOperationException(nameof(IdentityUser));
                }
            }
            private UserDetail CreateUserDetail()
            {
                try
                {
                    return Activator.CreateInstance<UserDetail>();
                }
                catch
                {
                    _logger.LogDebug($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                       $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                       $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");

                    throw new InvalidOperationException(nameof(IdentityUser));
                }
            }
            private IUserEmailStore<IdentityUser> GetEmailStore()
            {
                if (!_userManager.SupportsUserEmail)
                {
                    throw new NotSupportedException("The default UI requires a user store with email support.");
                }
                return (IUserEmailStore<IdentityUser>)_userStore;
            }
        }
    }
