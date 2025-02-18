using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;


namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ERPContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ERPContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return await Task.FromResult(product);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync(); 
        }

        public async Task<List<Product>> GetActiveProductsAsync()
        {
            return await _context.Products
                          .Where(p => p.Active) 
                          .ToListAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _context.Products
              .Where(p => p.ProductId == productId)
              .FirstOrDefaultAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _context.Products.AnyAsync(c => c.ProductId == productId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
