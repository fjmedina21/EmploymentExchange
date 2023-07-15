using System.ComponentModel.DataAnnotations;

namespace EmploymentExchangeAPI.Models
{
    public class RoleUserDTO
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid RoleId { get; set; }
    }
}
