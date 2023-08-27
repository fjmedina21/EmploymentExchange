﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmploymentExchangeAPI.Models
{
    public class LoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
