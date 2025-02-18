﻿using Microsoft.EntityFrameworkCore;
using ProductionOrderERP_API.ERP.Core.Entity;

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

        public ERPContext(DbContextOptions<ERPContext> options)
    : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Production_ERP");
        }
    }
}
