namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class FeatureFlag
    {
        public int FeatureFlagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ICollection<FeatureFlagTenant> FeatureFlagTenants { get; set; }
    }

    public class FeatureFlagTenant
    {
        public int FeatureFlagId { get; set; }
        public int TenantId { get; set; }
        public FeatureFlag FeatureFlag { get; set; }
    }
}
