using VideoAssetManager.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAssetManager.DataAccess.Repository.IRepository
{
    public interface IWrapperRepository
    {
        public IUserDetailRepository UserDetail { get; }
        public IUserMasterRepository UserMaster { get; }
        public ILoginLogOutRepository LoginLogOutMaster { get; }
        public IQuicklinkPermissionRepository QuicklinkPermission { get; }
        public ITabMenuPermissionRepository TabMenuPermission { get; }
        public ITabMenuesRepository TabMenu { get; }
     
        void Save(); 

        public IAspNetRolesRepository AspNetRoles { get; }
        public IAspNetUserRolesRepository AspNetUserRoles { get; }
        public IRawFootageRepository RawFootage { get; }
        public IEditedVideosRepository EditedVideos { get; }
        public IVMCategoryRepository VM_CategoryMaster { get; }
        public IProjectRepository VM_Project { get; }
        public IVMArtistMappingRepository VM_ArtistMapping { get; }

        //#region
        //public ISearchCollectionRepository SearchCollection { get; }

        //#endregion
    }
}