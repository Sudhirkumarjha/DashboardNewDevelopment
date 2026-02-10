using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAssetManager.DataAccess.Repository
{
    public class RawFootageRepository : Repository<VM_RawFootage>, IRawFootageRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public RawFootageRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base(VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }
        public void Save()
        {
            _VideoAssetManagerDBContext.SaveChanges();
        }
    }
}