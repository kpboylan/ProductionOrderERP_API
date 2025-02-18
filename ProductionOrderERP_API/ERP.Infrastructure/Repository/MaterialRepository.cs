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
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return await Task.FromResult(material);
        }

        public async Task<List<Material>> GetMaterialsAsync()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task<List<GetMaterialResponse>> GetMaterialsNewAsync()
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

        public async Task<Material?> GetMaterialAsync(int materialId)
        {
            return await _context.Materials
              .Where(p => p.MaterialID == materialId)
              .FirstOrDefaultAsync();
        }

        public async Task<Material> UpdateMaterialAsync(Material material)
        {
            try
            {
                _context.Materials.Update(material);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Exception: {UpdateMaterialAsync}", ex);
            }

            return material;
        }

        public async Task<List<MaterialType>> GetMaterialTypesAsync()
        {
            return await _context.MaterialTypes.ToListAsync();
        }

        public async Task<List<UOM>> GetUOMAsync()
        {
            return await _context.UOM.ToListAsync();
        }
    }
}
