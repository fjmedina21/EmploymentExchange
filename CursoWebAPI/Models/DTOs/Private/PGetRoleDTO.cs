namespace EmploymentExchange.Models.DTOs.Private
{
    public class PGetRoleDTO
    {
        public string Name { get; set; }
        public List<PGetRoleUsersDTO> Users { get; set; } = new();
    }
}
