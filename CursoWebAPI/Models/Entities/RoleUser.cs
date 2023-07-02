namespace EmploymentExchange.Models
{
    public class RoleUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
