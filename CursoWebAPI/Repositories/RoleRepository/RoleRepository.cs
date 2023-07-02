using EmploymentExchange.Data;
using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Repositories
{
    public class RoleRepository : IRole
    {
        private readonly MyDBContext dbContext;

        public RoleRepository(MyDBContext dBContext)
        {
            dbContext = dBContext;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await dbContext.Roles
                .OrderBy(e => e.Name)
                .Where(e => e.State)
                .ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(Guid id)
        {
            Role? role = await dbContext.Roles
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            return role == null ? null : role;
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            await dbContext.Roles.AddAsync(role);
            await dbContext.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> UpdateRoleAsync(Guid id, Role role)
        {
            Role? dbRole = await dbContext.Roles
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (dbRole == null) return null;

            dbRole.Name = role.Name;
            dbRole.Description = role.Description;
            dbRole.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return dbRole;
        }

        public async Task<Role?> DeleteRoleAsync(Guid id)
        {
            Role? roleExist = await dbContext.Roles
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (roleExist == null) return null;

            roleExist.State = false;
            await dbContext.SaveChangesAsync();

            return roleExist;
        }
    }
}
