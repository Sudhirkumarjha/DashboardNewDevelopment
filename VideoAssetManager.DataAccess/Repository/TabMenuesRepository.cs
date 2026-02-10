using VideoAssetManager.DataAccess.Repository.IRepository;
using VideoAssetManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAssetManager.DataAccess.Repository
{
    public class TabMenuesRepository : Repository<TabMenu>, ITabMenuesRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public TabMenuesRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base(VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }
        public void Save()
        {
            _VideoAssetManagerDBContext.SaveChanges();
        }
    }
}
