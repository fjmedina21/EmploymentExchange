﻿using EmploymentExchange.Data;
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

        public async Task<List<User>> GetUsersAsync()
        {
            return await dbContext.Users
                .OrderByDescending(e => e.UpdatedAt).ThenByDescending(e => e.CreatedAt)
                .Where(e => e.State)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            User? user = await dbContext.Users
                .Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id == id);

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
                .FirstOrDefaultAsync(e => e.Id == id);

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
                .FirstOrDefaultAsync(e => e.Id == id);

            if (userExist == null) return null;

            userExist.State = false;
            await dbContext.SaveChangesAsync();

            return userExist;
        }
    }
}
