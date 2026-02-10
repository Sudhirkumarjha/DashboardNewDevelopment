//using Microsoft.VisualStudio.Services.UserAccountMapping;
using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAssetManager.DataAccess.Repository
{
    public class AspNetRolesRepository : Repository<AspNetRoles>, IAspNetRolesRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public AspNetRolesRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base(VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }
        public void Save()
        {
            _VideoAssetManagerDBContext.SaveChanges();
        }
    }
}
