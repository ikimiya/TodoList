using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Users?> Register(string email, string password)
        {
            // existing user
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                return null;
            }

            var user = new Users
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public string? Login(string email, string password)
        {
            //find user
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)            {
                return null;
            }

            // password
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValid)
            {
                return null;
            }

            return GenerateToken(user);
        }

        private string GenerateToken(Users user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
