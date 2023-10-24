using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoleRepository : IRole
    {
        private readonly MyDBContext dbContext;

        public RoleRepository(MyDBContext dBContext)
        {
            dbContext = dBContext;
        }

        public async Task<(List<Role>, int)> GetRolesAsync()
        {
            IQueryable<Role> roles = dbContext.Roles.AsNoTracking()
                .OrderBy(e => e.Name).Where(e => e.State)//.Include(e => e.Users)
                .AsQueryable();

            List<Role> result = await roles.ToListAsync();
            int total = roles.Count();

            return (result, total);
        }

        public async Task<Role?> GetRoleByIdAsync(Guid id)
        {
            Role? role = await dbContext.Roles.AsNoTracking()
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return role is null ? null : role;
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
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbRole is null) return null;

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
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (roleExist is null) return null;

            roleExist.State = false;
            await dbContext.SaveChangesAsync();

            return roleExist;
        }
    }
}
