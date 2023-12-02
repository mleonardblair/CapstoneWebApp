using EcommerceApp.Client.Shared;
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
        public event Action OnChange;
        public string Message { get; set; }
        public string SnackMessage { get; set; }
        public Severity Severity { get; set; }
        ReusableResultSnackbar Snackbar { get; set; }
        public List<AppUserDto> AuthAdminUsers { get; set; }
        AppUserDto AuthUser { get; set; }
        Task GetUser(Guid? id);
        Task GetUserEmail();
        Task GetAllUserAdmin();
        Task<ServiceResponse<string>> RegisterUser(UserRegister registerRequest);
        Task<ServiceResponse<string>> LoginUser(UserLogin loginRequest);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword userChangePassword);
        Task<ServiceResponse<bool>> UpdateUser(AppUserDto userDto);
        Task<ServiceResponse<bool>> AddUser(AppUserDto userDto);
        Task<bool> IsUserAuthenticated();
        Task<string> GetUserRole();
    }
}
