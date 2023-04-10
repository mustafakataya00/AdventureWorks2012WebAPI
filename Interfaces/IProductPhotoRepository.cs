using AdventureWorkAPI.Models;

namespace AdventureWorkAPI.Interfaces
{
    public interface IProductPhotoRepository
    {
        Task<List<ProductPhoto>> GetAllProductPhotosAsync();
        Task<byte[]> GetProductThumbnail(int productId);
        Task<byte[]> GetProductLargePhoto(int productId);

    }
}
