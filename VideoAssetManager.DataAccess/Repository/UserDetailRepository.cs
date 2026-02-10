using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace VideoAssetManager.DataAccess.Repository
{
    public class UserDetailRepository : Repository<UserDetail>, IUserDetailRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public UserDetailRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base(VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }
        public void Save()
        {
            _VideoAssetManagerDBContext.SaveChanges();
        }
    }
}
