using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public interface IAuth
    {
        Task<(GetUserDTO?, string?)> LoginAsync(LoginDTO login);
        Task<GetUserDTO> SignupAsync();
        Task<User> ChangePasswordAsync();
        Task<User> ResetPasswordAsync();
        Task<User> ForgotPasswordAsync();
    }
}
