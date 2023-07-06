using EmploymentExchange.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentExchange.Repositories
{
    public interface IUser
    {
        Task<List<User>> GetUsersAsync(int pageNumber = 1, int pageSize = 100);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task<User?> DeleteUserAsync(Guid id);
        Task<User?> AssignRoleeAsync(Guid id);
        Task<User?> RevokeRoleeAsync(Guid id);
    }
}
