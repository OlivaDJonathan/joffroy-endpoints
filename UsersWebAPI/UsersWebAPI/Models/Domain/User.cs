namespace UsersWebAPI.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string email { get; set; }
        public required string phone { get; set; }
        public required string role { get; set; }
    }
}
