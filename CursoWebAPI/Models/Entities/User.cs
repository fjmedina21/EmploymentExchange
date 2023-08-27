using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmploymentExchangeAPI.Models
{
    [Index(nameof(Email), IsUnique = true, Name = "IX_Users_Email")]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(30)]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(30)]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress, StringLength(50)]
        public string Email { get; set; } = null!;
        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; set; } = null!;
        public string? Photo { get; set; }
        [Required]
        public bool State { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public IList<Role> Roles { get; set; } = new List<Role>();
        public IList<Job> Jobs { get; set; } = new List<Job>();
    }
}
