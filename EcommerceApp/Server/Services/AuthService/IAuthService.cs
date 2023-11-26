using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<Guid>> Register(ApplicationUser appUser, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(string userId, string password);
        Task<ServiceResponse<bool>> UpdateUser(string userId, string email, string firstName, string lastName);
        Guid GetUserId();
        string GetUserEmail();
        Task<ApplicationUser> GetApplicationUserByEmail(string email);
    }
}
