using AdventureWorkAPI.Models;

namespace AdventureWorkAPI.Interfaces
{
    public interface IInventoryRepository
    {
        Task<bool> CreateInventory(ProductInventory productInventory);
        Task<bool> UpdateProductInventory(int productId, int locationId, short quantity);
        Task<ICollection<Product>> GetAllProductsByShelf(string Shelf);
        Task<IDictionary<int, int>> GetAllProductQuantities();
        Task<bool> InventoryExists(int inventoryID);
        Task<bool> Save();
    }
}
