using AdventureWorkAPI.Data;
using AdventureWorkAPI.Interfaces;
using AdventureWorkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorkAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private AdventureWorks2012Context _context;

        public CategoryRepository(AdventureWorks2012Context context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExists(int productCategoryid)
        {
            return await _context.ProductCategories.AnyAsync(pc => pc.ProductCategoryId == productCategoryid);
        }

        public async Task<bool> CreateCategory(ProductCategory category)
        {
            _context.AddAsync(category);
            return await Save();
        }

        public async Task<bool> DeleteCategory(ProductCategory category)
        {
            _context.Remove(category);
            return await Save();
        }

        public async Task<ProductCategory> GetCategory(int categoryid)
        {
            return await _context.ProductCategories.Where(pc => pc.ProductCategoryId == categoryid).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Product>> GetProductByCategoryID(int categoryid)
        {
            return await _context.Products.Where(p => p.ProductSubcategory != null && p.ProductSubcategory.ProductCategoryId == categoryid).ToListAsync();
        }
        public async Task<bool> DeleteSubCategories(List<ProductSubcategory> ProductSubCategory)
        {
            _context.RemoveRange(ProductSubCategory);
            return await Save();
        }
        public async Task<bool> Save()
        {

            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;

        }

        public async Task<bool> UpdateCategory(ProductCategory category)
        {
            _context.Update(category);
            return await Save();
        }
    }
}
