using EcommerceApp.Server.Models;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<Guid>> Register(ApplicationUser appUser, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(Guid userId, string password);
    }
}
