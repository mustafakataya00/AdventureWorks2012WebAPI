using AdventureWorkAPI.Models;

namespace AdventureWorkAPI.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<List<ProductSubcategory>> GetSubCatByCatID(int categoryid);
        Task<bool> CreateSubCat(ProductSubcategory productSubcategory);
        Task<bool> Save();
    }
}
