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

        public string Message { get; set; } = "Loading...";
        public string SnackMessage { get; set; } = "THIS SHOULD NOT BE NULL SET IT TO SOMETHING";
        public Severity Severity { get; set; } = Severity.Error;
        public List<AppUserDto> AuthAdminUsers { get; set; }

        public event Action OnChange;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }
        public async Task GetAllUserAdmin()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<AppUserDto>>>("api/user/admin");
            if (response != null && response?.Data != null)
            {
                AuthAdminUsers = response.Data;

            }
            OnChange?.Invoke();
        }

        /// <summary>
        /// Asynchronously adds a new user to the system based on the provided user data transfer object (DTO).
        /// This method makes an HTTP POST request to the server with the specified user information. 
        /// It handles different response scenarios based on the server's response, including server validation errors,
        /// successful addition, and other potential errors. The method updates UI elements such as notifications (SnackMessage)
        /// and severity levels based on the outcome of the user addition process.
        /// </summary>
        /// <param name="appUserDto">The user data transfer object containing the user's information.</param>
        /// <returns>
        /// A ServiceResponse of type bool indicating the success or failure of the user addition operation.
        /// The response includes a data flag (true for success, false for failure), a success flag,
        /// a message providing details about the operation, and a status code indicating the type of response.
        /// </returns>
        /// <remarks>
        /// The method uses the HTTP client to send the user data to the 'api/user/admin' endpoint.
        /// Depending on the response status code and success flag, it sets appropriate UI elements
        /// and retrieves an updated user list if necessary. The method handles 404 status codes specifically
        /// for server validation errors, and general errors are handled in the else block.
        /// </remarks>
        public async Task<ServiceResponse<bool>> AddUser(AppUserDto appUserDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/admin", appUserDto);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            if (result.StatusCode == 404)
            {
                // If we hit a server validation, warn basically for invalid input.
                ServiceResponse<bool> users = new()
                {
                    Data = false,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Warning;
                SnackMessage = result.Message;
                await GetAllUserAdmin();
                return users;
            }
            if (result != null && result.Success == true)
            {
                ServiceResponse<bool> users = new()
                {
                    Data = false,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Success;
                SnackMessage = result.Message;
                await GetAllUserAdmin();

                return users;
            }
            else
            {
                ServiceResponse<bool> users = new() { Data = false, Success = false, Message = result.Message, StatusCode = result.StatusCode };
                Severity = Severity.Error;
                SnackMessage = result.Message;
                await GetAllUserAdmin();
                return users;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing user's information in the system using the provided user data transfer object (DTO).
        /// This method makes an HTTP PUT request to the server with the updated user information.
        /// It processes the server's response to determine the outcome of the update operation,
        /// updating UI elements such as notifications (SnackMessage) and severity levels accordingly.
        /// </summary>
        /// <param name="appUserDto">The user data transfer object containing the updated information for the user.</param>
        /// <returns>
        /// A ServiceResponse of type List<AppUserDto> indicating the success or failure of the user update operation.
        /// The response includes a data object (null in this implementation), a success flag,
        /// a message providing details about the operation, and a status code.
        /// </returns>
        /// <remarks>
        /// The method uses the HTTP client to send the updated user data to the 'api/user/admin' endpoint.
        /// If the update is successful (indicated by a true success flag in the response), it sets the UI elements
        /// for a successful operation and refreshes the user list. In case of failure, it sets the UI elements
        /// to reflect an error and also refreshes the user list. The method currently sets the Data property to null
        /// for both success and failure scenarios, which may need reconsideration based on intended functionality.
        /// </remarks>
        public async Task<ServiceResponse<bool>> UpdateUser(AppUserDto appUserDto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/user/admin", appUserDto);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            if (result != null && result.Success == true)
            {
                ServiceResponse<bool> users = new()
                {
                    Data = false,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Success;
                SnackMessage = result.Message;
                await GetAllUserAdmin();

                return users;
            }
            else
            {
                ServiceResponse<bool> users = new()
                {
                    Data = false,
                    Success = false,
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
                Severity = Severity.Error;
                SnackMessage = result.Message;
                await GetAllUserAdmin();
                return users;
            }


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

      
    }
}
