using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public interface IRole
    {
        Task<(List<Role>, int)> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(Guid id);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role?> UpdateRoleAsync(Guid id, Role role);
        Task<Role?> DeleteRoleAsync(Guid id);
        //Task<User> AssignRoleAsync(Guid id);
        //Task<User> RevokeRoleAsync(Guid id);
    }
}
