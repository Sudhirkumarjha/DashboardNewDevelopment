using Microsoft.EntityFrameworkCore;
using System.Linq;
using VideoAssetManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VideoAssetManager.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace VideoAssetManager.DataAccess
{
    public class VideoAssetManagerDBContext: IdentityDbContext
    {
        public VideoAssetManagerDBContext()
        {

        }
        public VideoAssetManagerDBContext(DbContextOptions<VideoAssetManagerDBContext> options) :base(options)
        {

        }       
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }
              
        public DbSet<QuickLinkMaster> QuickLinkMaster { get; set; }
        public DbSet<LoginLogOutMaster> LoginLogOutMaster { get; set; }
         public DbSet<TabMenu> TabMenu { get; set; }

        public virtual DbSet<AdminUsersFromMaster> AdminUsers { get; set; }

        public IQueryable<AdminUsersFromMaster> SearchAdminUsers()
        {
            return this.AdminUsers.FromSqlRaw("EXECUTE SP_GetAdminUsers");
        }
        public DbSet<Sp_GetTabMenu> GetTabMenu { get; set; }

        public DbSet<UserRoles> UserRole { get; set; }
        public new IQueryable<UserRoles> UserRoles(string RoleId, string Name)
        {
            return this.UserRole.FromSqlRaw("EXECUTE SP_GetUserRoles {0},{1}", RoleId, Name);
        }

        public DbSet<QuicklinkPermission> QuicklinkPermission { get; set; }
        public DbSet<GetQuickLinks> QuickLinks { get; set; }        
        public DbSet<TabMenuPermission> TabMenuPermission { get; set; }
        public DbSet<Sp_GetTabMenuPermission> GetTabMenuPermission { get; set; }
        public DbSet<SP_GetUserRole> GetUserRole { get; set; }       
  
        public DbSet<VM_RawFootage> VM_RawFootage { get; set; }
        public DbSet<VM_VideosResolution> VM_VideosResolution { get; set; }
        public DbSet<VM_EditedVideos> VM_EditedVideos { get; set; }
        public DbSet<VM_Project> VM_Project { get; set; }
        public DbSet<VM_Stage> VM_Stage { get; set; }
        public DbSet<VM_StageLookup> VM_StageLookup { get; set; }

        public DbSet<VM_CategoryMaster> VM_CategoryMaster { get; set; }
        public DbSet<VM_City> VM_City { get; set; }
        public DbSet<VM_ArtistMapping> VM_ArtistMapping { get; set; }

		public DbSet<ProjectOverview> ProjectOverview { get; set; }
		
       
       
        public DbSet<AspNetRoles> aspnetroles { get; set; }
        public DbSet<AspNetUserRoles> aspnetuserroles { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProjectOverview>().HasNoKey();
            // Configure the relationship between VM_EditedVideos and VM_ArtistMapping
            modelBuilder.Entity<VM_ArtistMapping>()
                .HasOne(p => p.EditedVideo)               // VM_ArtistMapping has one EditedVideo
                .WithMany(b => b.Participants)            // VM_EditedVideos has many Participants
                .HasForeignKey(p => p.ExportId)            // Foreign key in VM_ArtistMapping
                .HasPrincipalKey(b => b.ExportGuid);       // Primary key in VM_EditedVideos (ExportGuid)
        }
     
    }
}