using EcommerceApp.Server.Models;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.AuthService
{
    public interface IAuthService
    {
        /* event Action OnChange;
         Task<RegisterResponse> RegisterUser(RegisterRequest registerRequest);
         Task<LoginResponse> LoginUser(LoginRequest loginRequest);
         Task LogoutUser();
         Task<UserInfoResponse> GetUserInfo();*/
        Task<ServiceResponse<Guid>> RegisterUser(UserRegister registerRequest);
    }
}
