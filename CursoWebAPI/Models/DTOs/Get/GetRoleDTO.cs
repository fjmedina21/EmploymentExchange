﻿namespace EmploymentExchangeAPI.Models
{
    public class GetRoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
