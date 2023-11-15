using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EcommerceApp.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string email)
        {
            if(await _context.ApplicationUsers.AnyAsync(x => x.Email
            .ToLower()
            .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        public async Task<ServiceResponse<Guid>> Register(ApplicationUser appUser, string password)
        {
            if(await UserExists(appUser.Email))
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = "User already exists."
                };
            } else {
                CreatePasswordHashAndSalt(password, out byte[] passwordHash, out byte[] passwordSalt);

                appUser.PasswordHash = passwordHash;
                appUser.PasswordSalt = passwordSalt;

                _context.ApplicationUsers.Add(appUser);
                await _context.SaveChangesAsync();

                return new ServiceResponse<Guid>
                {
                    Data = appUser.Id,
                    Message = "User registered successfully."
                };
            }
        }
    }
}
