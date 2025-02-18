namespace ProductionOrderERP_API.ERP.Application.DTO
{
    public class CreateUserRequest
    {
        public int UserID { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int UserTypeID { get; set; } = 0;
    }
}
