﻿using EmploymentExchangeAPI.Data;
using EmploymentExchangeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchangeAPI.Repositories
{
    public class UserRepository : IUser
    {
        private readonly MyDBContext dbContext;

        public UserRepository(MyDBContext dBContext)
        {
            dbContext = dBContext;
        }

        public async Task<(List<User>, int)> GetUsersAsync(int pageNumber = 1, int pageSize = 50)
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

            return user is null ? null : user;
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
            User? dbUser = await dbContext.Users
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (dbUser is null || !dbUser.ComparePassword(user.Password)) return null;

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.Photo = user.Photo;
            dbUser.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return dbUser;
        }

        public async Task<User?> DeleteUserAsync(Guid id)
        {
            User? userExist = await dbContext.Users
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (userExist is null) return null;

            userExist.State = false;
            await dbContext.SaveChangesAsync();

            return userExist;
        }
    }
}
