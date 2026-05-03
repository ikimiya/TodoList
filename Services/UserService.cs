using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return (await _context.Users.ToListAsync());
        }

        public async Task<List<Users>> GetAllSelfUsers(int userID)
        {
            return (await _context.Users.Where(u => u.Id == userID).ToListAsync());
        }

        public async Task<Users?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Users> Create(Users user)
        {
            user.CreatedAt = DateTime.UtcNow;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await GetById(id);
            if (user == null)
            { 
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Users user)
        {
            var index = await _context.Users.FindAsync(user.Id);

            if(index == null)
            {
                return false;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }


        // User Profile
        public async Task<Users?> GetProfile(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateProfile(int userId, string email, string password)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            var duplicate = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower()
                                     && u.Id != userId);
            if (duplicate != null)
                throw new Exception("Email already in use");

            user.Email = email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
