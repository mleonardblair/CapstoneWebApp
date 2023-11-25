using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EcommerceApp.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetUserId() => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
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
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHashAndSalt(string password,  byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);

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

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if(user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if(!VerifyPasswordHashAndSalt(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
                response.Message = "User logged in successfully.";
            }
            
            return response;
        }

        /// <summary>
        /// When called this method creates a token for the user. The token contains two claims: NameIdentifier and Name.
        /// The token is created using the secret key stored in appsettings.json. The token is valid for 1 day.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(ApplicationUser user)
        {
            // Create new instance of claims with two claims: NameIdentifier and Name. Claims are used to store information about the user in the token.
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };  

            // Create new instance of symmetric security key using the secret key stored in appsettings.json
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                               .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            // Create new instance of signing credentials using the key above
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Create new instance of security token descriptor; The descriptor contains the claims, expiry date and signing credentials
           var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.Now.AddDays(1),
               signingCredentials: creds);


            // Create new instance of JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create new instance of token using the tokenHandler
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;

        }

        public async Task<ServiceResponse<bool>> ChangePassword(string userId, string newPassword)
        {
            var response = new ServiceResponse<bool>();
       
            // Convert the string userId to Guid
            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                response.Success = false;
                response.Message = "Invalid user ID.";
                return response;
            }

            var user = await _context.ApplicationUsers.FindAsync(userGuid);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }

            CreatePasswordHashAndSalt(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();
            response.Data = true;
            response.Message = "Password changed successfully.";

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateUser(string userId, string email, string firstName, string lastName)
        {
            var response = new ServiceResponse<bool>();

            // Convert the string userId to Guid
            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                response.Success = false;
                response.Message = "Invalid user ID.";
                return response;
            }
            var user = await _context.ApplicationUsers.FindAsync(userGuid);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }

            // Update non-sensitive fields only
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email ?? email;  // Avoid overwriting with null
            user.DateModified = DateTime.UtcNow;

            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "User updated successfully.";
            return response;
        }


        public async Task<ApplicationUser> GetApplicationUserByEmail(string email)
        {
            return await _context.ApplicationUsers.FirstOrDefaultAsync(i => i.Email.Equals(email));
        }
    }
}
