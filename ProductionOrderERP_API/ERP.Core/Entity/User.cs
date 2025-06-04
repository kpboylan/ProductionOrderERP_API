using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class User
    {
        [Key] 
        public int UserID { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required] 
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateModified { get; set; }

        // Add the UserTypeID foreign key
        public int UserTypeID { get; set; } // Assuming UserTypeID is required (NOT NULL in the database)

        // Navigation property to UserType (if you have a UserType entity)
        [ForeignKey("UserTypeID")]
        public virtual UserType UserType { get; set; }
        public int TenantID { get; set; }
    }
}
