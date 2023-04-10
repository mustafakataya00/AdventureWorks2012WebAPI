using AdventureWorkAPI.Data;
using AdventureWorkAPI.Interfaces;
using AdventureWorkAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AdventureWorkAPI.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private AdventureWorks2012Context _context;

        public InventoryRepository(AdventureWorks2012Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateInventory(ProductInventory productInventory)
        {
            _context.AddAsync(productInventory);
            return await Save();
        }
        public async Task<IDictionary<int, int>> GetAllProductQuantities()
        {
            var quantities = await _context.ProductInventories
                .GroupBy(pi => pi.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(pi => pi.Quantity) })
                .ToDictionaryAsync(p => p.ProductId, p => p.Quantity);

            return quantities;

        }

        public async Task<ICollection<Product>> GetAllProductsByShelf(string Shelf)
        {
            return await _context.ProductInventories
                .Where(pi => pi.Shelf == Shelf)
                .Join(_context.Products,
                      pi => pi.ProductId,
                      p => p.ProductId,
                      (pi, p) => p)
                .ToListAsync();
        }

        public async Task<bool> InventoryExists(int prodID)
        {

            return (await _context.ProductInventories?.AnyAsync(e => e.ProductId == prodID));
        }
    

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateProductInventory(int productId, int locationId, short quantity)
        {
            var productInventory = await _context.ProductInventories.SingleOrDefaultAsync(pi => pi.ProductId == productId && pi.LocationId == locationId);

            if (productInventory != null)
            {
                productInventory.Quantity = quantity;
                Save();
                return true;
            }

            return false;
        }


    }
}
