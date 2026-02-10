using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using VideoAssetManager.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAssetManager.DataAccess.Repository
{
    public class WrapperRepository : IWrapperRepository
    {

        private VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public WrapperRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
            UserDetail = new UserDetailRepository(_VideoAssetManagerDBContext);
            UserMaster = new UserMasterRepository(_VideoAssetManagerDBContext);
            LoginLogOutMaster = new LoginLogOutRepository(_VideoAssetManagerDBContext);            
            QuicklinkPermission = new QuicklinkPermissionRepository(_VideoAssetManagerDBContext);
            TabMenuPermission = new TabMenuPermissionRepository(_VideoAssetManagerDBContext);
            TabMenu = new TabMenuesRepository(_VideoAssetManagerDBContext);
            AspNetRoles = new AspNetRolesRepository(_VideoAssetManagerDBContext);
            AspNetUserRoles = new AspNetUserRolesRepository(_VideoAssetManagerDBContext);
            RawFootage = new RawFootageRepository(_VideoAssetManagerDBContext);
            EditedVideos = new EditedVideosRepository(_VideoAssetManagerDBContext);
            VM_Project = new ProjectRepository(_VideoAssetManagerDBContext);
            VM_CategoryMaster = new VMCategoryRepository(_VideoAssetManagerDBContext);
            VM_ArtistMapping = new VMArtistMappingRepository(_VideoAssetManagerDBContext);
         
        }

        public IUserDetailRepository UserDetail { get; private set; }
        public IUserMasterRepository UserMaster { get; private set; }  
        public ILoginLogOutRepository LoginLogOutMaster { get; private set; }   
        public IQuicklinkPermissionRepository QuicklinkPermission { get; private set; }
        public ITabMenuPermissionRepository TabMenuPermission { get; private set; }
        public ITabMenuesRepository TabMenu { get; private set; }
        public IAspNetRolesRepository AspNetRoles { get; private set; }
        public IAspNetUserRolesRepository AspNetUserRoles { get; private set; }
        public IRawFootageRepository RawFootage { get; private set; }
        public IEditedVideosRepository EditedVideos { get; private set; }
        public IProjectRepository VM_Project { get; private set; }
        public IVMCategoryRepository VM_CategoryMaster { get; private set; }
        public IVMArtistMappingRepository VM_ArtistMapping { get; private set; }

        public void Save()
        {
            _VideoAssetManagerDBContext.SaveChanges();
        }
    }
}