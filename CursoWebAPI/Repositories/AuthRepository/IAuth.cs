using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IAuth
    {
        Task<(GetUserDTO?,string?)> LogInAsync(LoginDTO login);
        Task<GetUserDTO> SignUpAsync();
        //Task<User> ChangePasswordAsync();
        //Task<User> ResetPasswordAsync();
        //Task<User> ForgotPasswordAsync();
    }
}
