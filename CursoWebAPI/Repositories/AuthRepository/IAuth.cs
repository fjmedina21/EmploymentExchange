using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IAuth
    {
        Task<LoggedInDTO?> LogInAsync(LoginDTO login);
        Task<LoggedInDTO> SignUpAsync();
        //Task<User> ChangePasswordAsync();
        //Task<User> ResetPasswordAsync();
        //Task<User> ForgotPasswordAsync();
    }
}
