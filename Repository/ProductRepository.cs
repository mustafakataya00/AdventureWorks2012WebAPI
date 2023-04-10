using AdventureWorkAPI.Data;
using AdventureWorkAPI.Interfaces;
using AdventureWorkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorkAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private AdventureWorks2012Context _context;

        public ProductRepository(AdventureWorks2012Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateProduct(Product product)
        {
             _context.AddAsync(product);
            return await Save();
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Remove(product);
            return await Save();
        }

        public async Task<Product> GetProduct(int productid)
        {
            return await _context.Products.Where(p => p.ProductId == productid).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Product>> GetProducts()
        {
            return await _context.Products.OrderBy(p => p.ProductId).Take(100).ToListAsync();
        }

        public async Task<bool> ProductExists(int productid)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == productid);
        }
        public async Task<bool> Save()
        {

            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;

        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                _context.Update(product);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ICollection<Product>> GetProductsBySubCatId(int subCategoryId)
        {
            var products = await _context.Products.OrderBy(p => p.ProductId).ToListAsync();
            var productsBySubID = new List<Product>();

            foreach (var product in products)
            {
                if (product.ProductSubcategoryId != null)
                {
                    var p = await _context.Products.Where(p => p.ProductSubcategoryId == subCategoryId).ToListAsync();
                    productsBySubID.AddRange(p);
                }
            }

            return productsBySubID;
        }


        public async Task<bool> DeleteProducts(ICollection<Product> productsToDelete)
        {
            try
            {
                _context.Products.RemoveRange(productsToDelete);
                await Save();
                return true;
             }
            catch(Exception)
            {
              return false;
            }
        }

        public async Task<ICollection<BillOfMaterial>> GetBillOfMaterial(int productId)
        {
            return await _context.BillOfMaterials.Where(bm => bm.ProductAssemblyId == productId).ToListAsync(); 
        }

        public async Task<ICollection<ProductCostHistory>> GetProductCostHistories(int productId)
        {
            return await _context.ProductCostHistories.Where(pch => pch.ProductId == productId).ToListAsync();
        }

        public async Task<ICollection<ProductInventory>> GetProductInventories(int productId)
        {
            return await _context.ProductInventories.Where(pi => pi.ProductId == productId).ToListAsync();
        }

        public async Task<ICollection<ProductListPriceHistory>> GetProductListPriceHistories(int productId)
        {
            return await _context.ProductListPriceHistories.Where(plph => plph.ProductId == productId).ToListAsync();
        }

        public async Task<ICollection<ProductProductPhoto>> GetProductProductPhotos(int productId)
        {     
            return await _context.ProductProductPhotos.Where(ppp => ppp.ProductId == productId).ToListAsync();
        }     
        public async Task<ICollection<ProductReview>> GetProductReviews(int productId)
        {     
            return await _context.ProductReviews.Where(pr => pr.ProductId == productId).ToListAsync();
        }      
        public async Task<ICollection<TransactionHistory>> GetTransactionHistories(int productId)
        {
            return await _context.TransactionHistories.Where(th => th.ProductId == productId).ToListAsync();
        }
        public async Task<ICollection<WorkOrder>> GetWorkOrders(int productId)
        {
            return await _context.WorkOrders.Where(wo => wo.ProductId == productId).ToListAsync();
        }

        public async Task<bool> DeleteBill(ICollection<BillOfMaterial> billOfMaterials)
        {
             try
            {
                _context.BillOfMaterials.RemoveRange(billOfMaterials);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCostHist(ICollection<ProductCostHistory> productCostHistories)
        {
            
            try
            {
                _context.ProductCostHistories.RemoveRange(productCostHistories);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductInv(ICollection<ProductInventory> productInventories)
        {
            
            try
            {
                _context.ProductInventories.RemoveRange(productInventories);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductListHist(ICollection<ProductListPriceHistory> productListPriceHistorie)
        {
            try
            {
                _context.ProductListPriceHistories.RemoveRange(productListPriceHistorie);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductPhoto(ICollection<ProductProductPhoto> productProductPhotoes)
        {
            try
            {
                _context.ProductProductPhotos.RemoveRange(productProductPhotoes);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductReview(ICollection<ProductReview> productReviews)
        {
            try
            {
                _context.ProductReviews.RemoveRange(productReviews);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTransactionHist(ICollection<TransactionHistory> transactionHistories)
        {
            try
            {
                _context.TransactionHistories.RemoveRange(transactionHistories);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteWorkOrder(ICollection<WorkOrder> workOrders)
        {
            try
            {
                _context.WorkOrders.RemoveRange(workOrders);
                await Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
