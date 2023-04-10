using AdventureWorkAPI.Models;

namespace AdventureWorkAPI.Interfaces
{
    public interface ICategoryRepository
    { 
        Task<ICollection<Product>> GetProductByCategoryID(int categoryid);
        Task<ProductCategory> GetCategory(int categoryid);
        Task<bool> CategoryExists(int productCategoryid);
        Task<bool> CreateCategory(ProductCategory category);
        Task<bool> UpdateCategory(ProductCategory category);
        Task<bool> DeleteCategory(ProductCategory category);
        Task<bool> DeleteSubCategories(List<ProductSubcategory> ProductSubCategory);
        Task<bool> Save();
    }
}
