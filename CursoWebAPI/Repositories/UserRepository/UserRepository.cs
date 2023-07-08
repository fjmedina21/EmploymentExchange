using EmploymentExchange.Data;
using EmploymentExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Repositories
{
    public class UserRepository : IUser
    {
        private readonly MyDBContext dbContext;

        public UserRepository(MyDBContext dBContext)
        {
            dbContext = dBContext;
        }

        public async Task<(List<User>,int)> GetUsersAsync(int pageNumber = 1, int pageSize = 50)
        {
            //pagination
            int skipResults = (pageNumber - 1) * pageSize;

            IQueryable<User> users = dbContext.Users.AsNoTracking()
                .OrderByDescending(e => e.UpdatedAt).ThenByDescending(e => e.CreatedAt)
                .Where(e => e.State)
                .Include(e => e.RoleUser).ThenInclude(e => e.Roles)              
                .AsQueryable();

            List<User> result = await users.Skip(skipResults).Take(pageSize).ToListAsync();
            int total = users.Count();

            return (result, total);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            User? user = await dbContext.Users.AsNoTracking()
                .Where(e => e.State)
                .Include(e => e.RoleUser).ThenInclude(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return user == null ? null : user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.HashPassword(user.Password);
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateUserAsync(Guid id, User user)
        {
            User? dbUsers = await dbContext.Users
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbUsers == null || !dbUsers.ComparePassword(user.Password)) return null;

            dbUsers.FirstName = user.FirstName;
            dbUsers.LastName = user.LastName;
            dbUsers.Email = user.Email;
            dbUsers.Photo = user.Photo;
            dbUsers.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return dbUsers;
        }

        public async Task<User?> DeleteUserAsync(Guid id)
        {
            User? userExist = await dbContext.Users
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (userExist == null) return null;

            userExist.State = false;
            await dbContext.SaveChangesAsync();

            return userExist;
        }

        public Task<User> AssignRoleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> RevokeRoleAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
