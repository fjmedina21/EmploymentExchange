using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public interface IUser
    {
        Task<(List<User>, int)> GetUsersAsync(int pageNumber, int pageSize);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task<User?> DeleteUserAsync(Guid id);
    }
}
