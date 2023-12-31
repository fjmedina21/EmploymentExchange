﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(30)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        [Required]
        public bool State { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public IList<User> Users { get; set; } = new List<User>();

    }
}
