

using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Service
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<List<GetProductRequest>> GetActiveProductsAsync();
        Task<List<GetProductRequest>> GetAllProductsAsync();
        Task<GetProductRequest> GetProductsAsync(int productId);
        Task<Product> UpdateProductAsync(int productId, UpdateProductRequest product);
    }
}