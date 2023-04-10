using AdventureWorkAPI.Models;

namespace AdventureWorkAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetProducts();
        Task<Product> GetProduct(int productId);
        Task<ICollection<BillOfMaterial>> GetBillOfMaterial(int productId);
        Task<ICollection<ProductCostHistory>> GetProductCostHistories(int productId);
        Task<ICollection<ProductInventory>> GetProductInventories(int productId);
        Task<ICollection<ProductListPriceHistory>> GetProductListPriceHistories(int productId);
        Task<ICollection<ProductProductPhoto>> GetProductProductPhotos(int productId);
        Task<ICollection<ProductReview>> GetProductReviews(int productId);
        Task<ICollection<TransactionHistory>> GetTransactionHistories(int productId);
        Task<ICollection<WorkOrder>> GetWorkOrders(int productId);
        Task<bool> DeleteBill(ICollection<BillOfMaterial>billOfMaterials);
        Task<bool> DeleteCostHist(ICollection<ProductCostHistory> productCostHistories);
        Task<bool> DeleteProductInv(ICollection<ProductInventory> productInventories);
        Task<bool> DeleteProductListHist(ICollection<ProductListPriceHistory> productListPriceHistorie);
        Task<bool> DeleteProductPhoto(ICollection<ProductProductPhoto> productProductPhotoes);
        Task<bool> DeleteProductReview(ICollection<ProductReview> productReviews);
        Task<bool> DeleteTransactionHist(ICollection<TransactionHistory> transactionHistories);
        Task<bool> DeleteWorkOrder(ICollection<WorkOrder> workOrders);
        Task<bool> ProductExists(int productId);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Product product);
        Task<bool> Save();
        Task<ICollection<Product>> GetProductsBySubCatId(int subCategoryId);
        Task<bool> DeleteProducts(ICollection<Product> productsToDelete);
    }

}
