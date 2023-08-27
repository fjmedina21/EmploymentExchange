using System.ComponentModel.DataAnnotations;

namespace EmploymentExchangeAPI.Models
{
    public class RoleDTO
    {
        [Required, MaxLength(30, ErrorMessage = "Name must have maximum 30 characters")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
