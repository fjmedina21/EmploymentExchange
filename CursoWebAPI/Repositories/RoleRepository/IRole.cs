using EmploymentExchange.Models;

namespace EmploymentExchange.Repositories
{
    public interface IRole
    {
        Task<List<Role>> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(Guid id);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role?> UpdateRoleAsync(Guid id, Role role);
        Task<Role?> DeleteRoleAsync(Guid id);
    }
}
