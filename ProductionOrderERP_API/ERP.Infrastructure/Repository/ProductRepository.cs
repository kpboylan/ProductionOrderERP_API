using AutoMapper;
using Microsoft.Data.SqlClient;
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
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return await Task.FromResult(product);
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> GetActiveProductsAsync()
        {
            try
            {
                return await _context.Products
                              .Where(p => p.Active)
                              .ToListAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            try
            {
                return await _context.Products
                  .Where(p => p.ProductId == productId)
                  .FirstOrDefaultAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            try
            {
                return await _context.Products.AnyAsync(c => c.ProductId == productId);
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() >= 0);
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }
    }
}
