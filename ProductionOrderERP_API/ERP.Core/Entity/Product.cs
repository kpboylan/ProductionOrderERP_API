using System.ComponentModel.DataAnnotations;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductCode { get; set; }
        public bool Active { get; set; }
    }
}
