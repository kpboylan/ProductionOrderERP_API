using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Interface
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<List<Product>> GetActiveProductsAsync();
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task<bool> ProductExistsAsync(int productId);
        Task<bool> SaveChangesAsync();
        Task<Product> UpdateProductAsync(Product product);
    }
}