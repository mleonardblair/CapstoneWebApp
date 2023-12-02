using AutoMapper;
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
using System.Text.RegularExpressions;

namespace EcommerceApp.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext context, 
            IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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
                new Claim(ClaimTypes.Role, user.Role)
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


        public async Task<ApplicationUser?> GetApplicationUserByEmail(string email)
        {
            return await _context.ApplicationUsers.FirstOrDefaultAsync(i => i.Email.Equals(email));
        }


        public async Task<ServiceResponse<List<AppUserDto>>> GetAllUserAdmin()
        {
            var response = new ServiceResponse<List<AppUserDto>>();
            var users = await _context.ApplicationUsers.ToListAsync();

            if (users == null || !users.Any())
            {
                response.Success = false;
                response.Message = "Users are not found.";
            }
            else
            {
                var convertedCat = _mapper.Map<List<AppUserDto>>(users);
                response.Data = convertedCat;
                response.Message = "All went well.";
            }
            return response;
        
        }

        public async Task<ServiceResponse<AppUserDto>>GetUserByIdAdmin(Guid userId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates the details of an existing user in the application database.
        /// </summary>
        /// <remarks>
        /// Performs various checks before updating a user:
        /// - Validates that the first and last names contain only allowed characters (letters and specific French characters).
        /// - Ensures the uniqueness of the user's email address.
        /// - Checks for the existence of a user with the same first or last name.
        /// - Confirms the existence of the user in the database by matching the user ID.
        /// </remarks>
        /// <param name="appUser">An <see cref="AppUserDto"/> object containing the user's updated information.</param>
        /// <returns>
        /// A <see cref="ServiceResponse{T}"/> object with a boolean data value.
        /// The response indicates success or failure, a descriptive message, and an HTTP status code.
        /// </returns>
        /// <exception cref="Exception">Thrown when an error occurs during the database update process.</exception>

        public async Task<ServiceResponse<bool>> UpdateUser(AppUserDto appUser)
        {
                // Validate the category name (only letters, no digits or special characters but does take french characters)
                if (!Regex.IsMatch(appUser.FirstName, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$") ||
                    !Regex.IsMatch(appUser.LastName, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$"))
                {
                return new ServiceResponse<bool>
                {
                        Success = false,
                        Message = "Invalid user name. Only letters are allowed.",
                        Data = false,
                        StatusCode = 400 // Bad Request
                    };
                }
                // Check for email uniqueness only on non-deleted users as if deleted they wont be visible so we dont want confusion with the user names being taken they cant see.
               
                // get the id of the user who has an email that is the same as the email passed in appUser
                var userId = await _context.ApplicationUsers
                                .Where(c => c.Email == appUser.Email)
                                .Select(c => c.Id)
                                .FirstOrDefaultAsync();
            // if the user id is not the same as the id passed in appUser then the email is already taken
                var isEmailUsed = userId != appUser.Id && userId != Guid.Empty;
                if (isEmailUsed)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "User email already exists.",
                        Data = false,
                        StatusCode = 400 // Bad Request
                    };
                }
             
                // Check if the user already exists
                var dbUser = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.Id == appUser.Id);
                if (dbUser == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "User not found.",
                        Data = false,
                        StatusCode = 404 // Not Found
                    };
                }

                // Update user properties
                dbUser.FirstName = appUser.FirstName;
                dbUser.LastName = appUser.LastName;
           /*     dbCategory.Visible = appUser.Visible;*/
                dbUser.DateModified = DateTime.UtcNow;
                dbUser.Role = appUser.Role;
                dbUser.Email = appUser.Email;
                
             // update the user in the database acording to the user we have 
             _context.ApplicationUsers.Update(dbUser);
                try
                {
                    await _context.SaveChangesAsync();
                    return new ServiceResponse<bool>
                    {
                        Success = true,
                        Message = "User updated successfully.",
                        Data = true,
                        StatusCode = 200 // OK
                    };
                }
                catch (Exception)
                {
                    // Log the exception details here

                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "A server error occurred while updating the user.",
                        Data = false,
                        StatusCode = 500 // Internal Server Error
                    };
                }
            

        }

        public async Task<ServiceResponse<bool>> AddUser(AppUserDto appUser)
        { // Validate the user name (only letters, no digits or special characters but does take french characters)
            if (!Regex.IsMatch(appUser.LastName, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$") ||
                !Regex.IsMatch(appUser.FirstName, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$"))

            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Invalid user name. Only letters are allowed.",
                    Data = false,
                    StatusCode = 400 // Bad Request
                };
            }

            var isEmailUsed = await _context.ApplicationUsers
                                .AnyAsync(c => c.Email == appUser.Email);

            if (isEmailUsed)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User email already exists.",
                    Data = false,
                    StatusCode = 400 // Bad Request
                };
            }
            // Check for name uniqueness
            var isNameUsed = await _context.ApplicationUsers
                                .AnyAsync(c => c.FirstName == appUser.FirstName &&
                                                               c.LastName == appUser.LastName);
            if (isNameUsed)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User name already exists.",
                    Data = false,
                    StatusCode = 400 // Bad Request
                };
            }
            var response = new ServiceResponse<bool>
            {
                Data = new bool()
            };
            ApplicationUser a = new();
            try
            {
                a = _mapper.Map<ApplicationUser>(appUser);
                _context.ApplicationUsers.Add(a);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    response.Data = false;
                    response.Success = false;
                    response.Message = "Failed to add users.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }
                else
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Users added successfully.";
                    response.StatusCode = 200; // OK
                    return response;
                }
            }
            catch (Exception)
            {
                // Log the exception details here

                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "An unexpected server error occurred while adding the user.",
                    Data = false,
                    StatusCode = 500 // Internal Server Error
                };
            }

        }

        public Task GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<AppUserDto>> GetUserById(Guid userId)
        {
            var response = new ServiceResponse<AppUserDto>();
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(i => i.Id == userId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                response.StatusCode = 404;
            }
            else
            {
                var convertedUser = _mapper.Map<AppUserDto>(user);
                response.Data = convertedUser;
                response.Message = "User found.";
                response.Success = true;
                response.StatusCode = 200;
            }
            return response;
        }

     
    }
}
