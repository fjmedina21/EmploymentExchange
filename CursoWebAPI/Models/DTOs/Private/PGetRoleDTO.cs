namespace EmploymentExchange.Models.DTOs.Private
{
    public class PGetRoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<PGetRoleUsers> Users { get; set; }
    }
}
