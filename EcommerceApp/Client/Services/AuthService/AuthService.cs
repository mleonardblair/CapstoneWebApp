using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace EcommerceApp.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword userChangePassword)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", userChangePassword);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            if (response != null && response?.Data != null)
            {
                return response;
            }
            else
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = response.Message
                };
            }
        }
        // get the user role from the claims principal that is currently logged in
        public async Task<string> GetUserRole()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var userRole = authState.User.FindFirst(c => c.Type == ClaimTypes.Role);
            return userRole?.Value;
        }
        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> LoginUser(UserLogin loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if (response != null && response?.Data != null)
            {
                return response;
            }
            else
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = response.Message
                };
            }
        }

        public async Task<ServiceResponse<string>> RegisterUser(UserRegister registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if (response != null && response?.Data != null)
            {
                return response;
            }
            else
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Something went wrong during registration."
                };
            }
        }

        public async Task<ServiceResponse<bool>> UpdateUser(AppUserDto userDto)
        {

            var result = await _httpClient.PutAsJsonAsync("api/auth/update", userDto);


            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            if (response != null && response?.Data != null)
            {
                return response;
            }
            else
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Something went wrong during update."
                };
            }
        }
    }
}
