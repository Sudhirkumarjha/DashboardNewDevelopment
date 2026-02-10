using VideoAssetManager.Models;
using VideoAssetManager.DataAccess.Repository.IRepository;

namespace VideoAssetManager.DataAccess.Repository
{
    public class VMArtistMappingRepository : Repository<VM_ArtistMapping>, IVMArtistMappingRepository
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        public VMArtistMappingRepository(VideoAssetManagerDBContext VideoAssetManagerDBContext) : base(VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
        }
        public void Save()
        {
            _VideoAssetManagerDBContext.SaveChanges();
        }
    }
}