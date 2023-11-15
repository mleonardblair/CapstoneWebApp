using EcommerceApp.Server.Models;
using EcommerceApp.Shared.Models;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
    }
}
