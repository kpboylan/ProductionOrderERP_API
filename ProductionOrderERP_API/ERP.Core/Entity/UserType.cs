using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class UserType
    {
        [Key]
        public int UserTypeID { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Type { get; set; }

        [Required]
        public bool Active { get; set; }

        // Navigation property to Users
        public virtual ICollection<User> Users { get; set; }
    }
}
