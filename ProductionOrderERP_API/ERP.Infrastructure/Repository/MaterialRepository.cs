using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;
using Serilog;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ERPContext _context;

        public MaterialRepository(ERPContext context)
        {
            _context = context;
        }

        public async Task<Material> CreateMaterialAsync(Material material)
        {
            try
            {
                _context.Materials.Add(material);
                await _context.SaveChangesAsync();

                return await Task.FromResult(material);
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

        public async Task<List<Material>> GetMaterialsAsync()
        {
            try
            {
                return await _context.Materials.ToListAsync();
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

        public async Task<List<GetMaterialResponse>> GetMaterialsNewAsync()
        {
            try
            {
                return await (from material in _context.Materials
                              join uom in _context.UOM on material.UOMId equals uom.UOMID
                              join materialType in _context.MaterialTypes on material.MaterialType equals materialType.MaterialTypeID
                              select new GetMaterialResponse
                              {
                                  MaterialID = material.MaterialID,
                                  MaterialType = material.MaterialType,
                                  UOMId = material.UOMId,
                                  MaterialName = material.MaterialName,
                                  Description = material.Description,
                                  CurrentStock = material.CurrentStock,
                                  UOMCode = uom.UOMCode,
                                  MaterialTypeAbbreviation = materialType.MaterialTypeAbbreviation,

                              }).ToListAsync();
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

        public async Task<List<GetMaterialResponse>> GetActiveMaterialsNewAsync()
        {
            try
            {
                return await (from material in _context.Materials.Where(p => p.Active)
                              //where material.Active 
                              join uom in _context.UOM on material.UOMId equals uom.UOMID
                              join materialType in _context.MaterialTypes on material.MaterialType equals materialType.MaterialTypeID
                              select new GetMaterialResponse
                              {
                                  MaterialID = material.MaterialID,
                                  MaterialType = material.MaterialType,
                                  UOMId = material.UOMId,
                                  MaterialName = material.MaterialName,
                                  Description = material.Description,
                                  CurrentStock = material.CurrentStock,
                                  UOMCode = uom.UOMCode,
                                  MaterialTypeAbbreviation = materialType.MaterialTypeAbbreviation,
                                  Active = material.Active
                              })
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

        public async Task<Material?> GetMaterialAsync(int materialId)
        {
            try
            {
                return await _context.Materials
                  .Where(p => p.MaterialID == materialId)
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

        public async Task<Material> UpdateMaterialAsync(Material material)
        {
            try
            {
                _context.Materials.Update(material);
                await _context.SaveChangesAsync();

                return material;
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

        public async Task<List<MaterialType>> GetMaterialTypesAsync()
        {
            try
            {
                return await _context.MaterialTypes.ToListAsync();
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

        public async Task<List<UOM>> GetUOMAsync()
        {
            try
            {
                return await _context.UOM.ToListAsync();
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
