using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Threading.Tasks;

namespace EcommerceApp.Client.Services.AuthService
{
    public interface IAuthService
    {
        /* event Action OnChange;
         Task<RegisterResponse> RegisterUser(RegisterRequest registerRequest);
         Task<LoginResponse> LoginUser(LoginRequest loginRequest);
         Task LogoutUser();
         Task<UserInfoResponse> GetUserInfo();*/
        Task<ServiceResponse<string>> RegisterUser(UserRegister registerRequest);
        Task<ServiceResponse<string>> LoginUser(UserLogin loginRequest);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword userChangePassword);
        Task<ServiceResponse<bool>> UpdateUser(AppUserDto userDto);
        Task<bool> IsUserAuthenticated();
    }
}
