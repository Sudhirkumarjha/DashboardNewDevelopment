using VideoAssetManager.Models;
using VideoAssetManager.DataAccess.Repository.IRepository;

namespace VideoAssetManager.DataAccess.Repository
{
    public class VMCategoryRepository : Repository<VM_CategoryMaster>, IVMCategoryRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public VMCategoryRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base(VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }
        public void Save()
        {
            _VideoAssetManagerDBContext.SaveChanges();
        }
    }
}
