using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IUser
    {
        Task<List<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task<User?> DeleteUserAsync(Guid id);
    }
}
