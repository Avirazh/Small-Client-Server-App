namespace Server.DataAccess.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }       
    }
}