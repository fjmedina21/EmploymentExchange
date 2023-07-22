namespace EmploymentExchangeAPI.Models
{
    public class RoleUser
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public User Users { get; set; }
        public Role Roles { get; set; }
    }
}
