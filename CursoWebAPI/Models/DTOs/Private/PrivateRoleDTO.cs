namespace EmploymentExchange.Models.DTOs.Private
{
    public class PrivateRoleDTO
    {
        public string Name { get; set; }
        public List<PrivateRoleUsersDTO> Users { get; set; } = new();
    }
}
