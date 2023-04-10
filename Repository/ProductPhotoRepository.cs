using AdventureWorkAPI.Data;
using AdventureWorkAPI.Interfaces;
using AdventureWorkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorkAPI.Repository
{
    public class ProductPhotoRepository : IProductPhotoRepository
    {
        private AdventureWorks2012Context _context;

        public ProductPhotoRepository(AdventureWorks2012Context context)
        {
            _context = context;
        }
        public async Task<List<ProductPhoto>> GetAllProductPhotosAsync()
        {
            return await _context.ProductPhotos.ToListAsync();
        }
        public async Task<byte[]> GetProductThumbnail(int productId)
        {
            var productPhoto = await _context.ProductProductPhotos
                .SingleOrDefaultAsync(p => p.ProductId == productId);

            if (productPhoto == null)
            {
                return null;
            }

            var photoId = productPhoto.ProductPhotoId;

            var thumbnail = await _context.ProductPhotos
                .Where(p => p.ProductPhotoId == photoId)
                .Select(p => p.ThumbNailPhoto)
                .SingleOrDefaultAsync();

            return thumbnail;
        }

        public async Task<byte[]> GetProductLargePhoto(int productId)
        {
            var productPhoto = await _context.ProductProductPhotos
                .SingleOrDefaultAsync(p => p.ProductId == productId);

            if (productPhoto == null)
            {
                return null;
            }

            var photoId = productPhoto.ProductPhotoId;

            var thumbnail = await _context.ProductPhotos
                .Where(p => p.ProductPhotoId == photoId)
                .Select(p => p.LargePhoto)
                .SingleOrDefaultAsync();

            return thumbnail;
        }

    }
}
