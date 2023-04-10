using AdventureWorkAPI.Data;
using AdventureWorkAPI.Interfaces;
using AdventureWorkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorkAPI.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
             private AdventureWorks2012Context _context;

        public SubCategoryRepository(AdventureWorks2012Context context)
        {
            _context = context;
        }
        public async Task<bool> Save()
        {

            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;

        }
        public async Task<bool> CreateSubCat(ProductSubcategory productSubcategory)
        {

            await _context.AddAsync(productSubcategory);
            return await Save();
        }

        public async Task<List<ProductSubcategory>> GetSubCatByCatID(int categoryid)
        {
            return await _context.ProductSubcategories.Where(psc => psc.ProductCategoryId == categoryid).ToListAsync();
        }
    }
}
