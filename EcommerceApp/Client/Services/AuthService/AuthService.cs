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
        public async Task<ServiceResponse<Guid>> RegisterUser(UserRegister registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<Guid>>();
            if (response != null && response?.Data != null)
            {
                return response;
            }
            else
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = "Something went wrong during registration."
                };
            }
        }
    }
}
