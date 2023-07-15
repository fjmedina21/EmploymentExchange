using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Models.ManyToMany;

namespace EmploymentExchangeAPI.Repositories
{
    public interface IRole
    {
        Task<(List<Role>, int)> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(Guid id);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role?> UpdateRoleAsync(Guid id, Role role);
        Task<Role?> DeleteRoleAsync(Guid id);
        Task<RoleUser> AssignRoleAsync(RoleUser entity);
        void RevokeRole(RoleUser entity);
    }
}
