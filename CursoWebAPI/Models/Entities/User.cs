using EmploymentExchange.Models.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BC = BCrypt.Net.BCrypt;

namespace EmploymentExchange.Models
{
    [Index(nameof(Email), IsUnique = true, Name = "IX_Users_Email")]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(30)]
        public string FirstName { get; set; }
        [Required, StringLength(30)]
        public string LastName { get; set; }
        [Required, EmailAddress,StringLength(50)]
        public string Email { get; set; }
        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; set; }
        public string? Photo { get; set; }
        [Required]
        public bool State { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public IList<RoleUser> RoleUser { get; set; } 
        public ICollection<Job> Jobs { get; set; } 
        
        public string HashPassword(string password)
        {
            return Password = BC.EnhancedHashPassword(password, 15);
        }

        public bool ComparePassword(string password)
        {
            return BC.EnhancedVerify(password, Password);
        }
    }
}
