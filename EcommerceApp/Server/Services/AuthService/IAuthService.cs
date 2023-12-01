using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.AuthService
{
    public interface IAuthService
    {
        // get all roles
        Task<ServiceResponse<Guid>> Register(ApplicationUser appUser, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(string userId, string password);
        Task<ServiceResponse<bool>> UpdateUser(string userId, string email, string firstName, string lastName);
        Guid GetUserId();
        string GetUserEmail();
        Task<ApplicationUser?> GetApplicationUserByEmail(string email);
        Task GetUsers();

        // Gets the user by the id passed in the request body and returns the user's information populating the state on the auth service for the logged in user.
        Task<ServiceResponse<AppUserDto>> GetUserById(Guid userId);

        Task<ServiceResponse<List<AppUserDto>>> GetAllUserAdmin();
        Task<ServiceResponse<AppUserDto>> GetUserByIdAdmin(Guid userId);
        Task<ServiceResponse<bool>> UpdateUser(AppUserDto appUserDto);
        Task<ServiceResponse<bool>> AddUser(AppUserDto appUserDto);

    }
}
