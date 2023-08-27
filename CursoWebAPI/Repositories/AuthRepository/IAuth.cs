﻿using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public interface IAuth
    {
        Task<(GetUserDTO?, string?)> LoginAsync(LoginDTO login);
        Task<GetUserDTO> SignupAsync();
        Task<APIResponse> ChangePasswordAsync(ChangePasswordDTO model, string token);
        Task<User> ResetPasswordAsync();
        Task<User> ForgotPasswordAsync();
    }
}
