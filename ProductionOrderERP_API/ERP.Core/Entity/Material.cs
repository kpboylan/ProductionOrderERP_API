using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string? MaterialName { get; set; } 
        public string? Description { get; set; }

        //public string? UOMCode { get; set; }

        //public string? MaterialTypeAbbreviation { get; set; }
        public decimal CurrentStock { get; set; }

        public bool Active { get; set; }

        // foreign keys
        public int UOMId { get; set; }
        public int MaterialType { get; set; }
    }
}
