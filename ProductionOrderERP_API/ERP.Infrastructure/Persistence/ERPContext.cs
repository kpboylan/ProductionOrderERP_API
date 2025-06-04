using Microsoft.EntityFrameworkCore;
using ProductionOrderERP_API.ERP.Core.Entity;
using Npgsql.EntityFrameworkCore;

namespace ProductionOrderERP_API.ERP.Infrastructure.Persistence
{
    public class ERPContext : DbContext
    {
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<PackagingMaterial> FinishedMaterials { get; set; }
        public DbSet<ProductionBatch> ProductionBatches { get; set; }
        public DbSet<ProductionOrderProcess> ProductionOrderProcesses { get; set; }
        public DbSet<ProductionOrderRawMaterial> ProductionOrderRawMaterials { get; set; }
        public DbSet<ProductionProcess> ProductionProcesses { get; set; }
        public DbSet<QualityControl> QualityControls { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<UOM> UOM { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }

        public DbSet<SensorData> SensorReadings { get; set; }
        public DbSet<HumidityReading> HumidityReadings { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<FactoryRoom> FactoryRooms { get; set; }
        public DbSet<PendingQueueMessage> PendingQueueMessages { get; set; }

        public DbSet<FeatureFlag> FeatureFlags { get; set; }
        public DbSet<FeatureFlagTenant> FeatureFlagTenants { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public ERPContext(DbContextOptions<ERPContext> options)
    : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeatureFlagTenant>()
                .HasKey(ft => new { ft.FeatureFlagId, ft.TenantId });

            modelBuilder.Entity<FeatureFlagTenant>()
                .HasOne(ft => ft.FeatureFlag)
                .WithMany(f => f.FeatureFlagTenants)
                .HasForeignKey(ft => ft.FeatureFlagId);
        }
    }
}
