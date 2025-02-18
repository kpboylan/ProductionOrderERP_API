namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class Login
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SubmittedPassword { get; set; }
        public string HashedPassword { get; set; }
    }
}
