﻿namespace EmploymentExchangeAPI.Models.Private
{
    public class PrivateUserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<PrivateUserRolesDTO> Roles { get; set; }
    }
}
