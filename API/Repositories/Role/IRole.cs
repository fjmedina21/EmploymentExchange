using API.Models;

namespace API.Repositories
{
    public interface IRole
    {
        Task<(List<Role>, int)> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(Guid id);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role?> UpdateRoleAsync(Guid id, Role role);
        Task<Role?> DeleteRoleAsync(Guid id);
    }
}
