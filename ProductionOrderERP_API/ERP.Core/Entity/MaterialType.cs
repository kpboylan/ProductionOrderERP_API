using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class MaterialType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialTypeID { get; set; }

        [Required]
        [MaxLength(100)] 
        public string MaterialTypeName { get; set; }

        [Required]
        [MaxLength(10)]   
        public string MaterialTypeAbbreviation { get; set; }

      
        [Required]
        public bool IsActive { get; set; }
    }
}