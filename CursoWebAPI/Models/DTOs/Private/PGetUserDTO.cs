namespace EmploymentExchange.Models.DTOs.Private
{
    public class PGetUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; }
        public List<PGetUserRolesDTO> Roles { get; set; }
    }
}
