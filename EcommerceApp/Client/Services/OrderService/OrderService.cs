using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient httpClient,
            AuthenticationStateProvider authStateProvider,
            NavigationManager navigationManager) {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
        public async Task PlaceOrderAsync()
        {
            if(await IsUserAuthenticated())
            {
                var response = await _httpClient.PostAsync("api/orders", null);
                if (response.IsSuccessStatusCode)
                {
                    _navigationManager.NavigateTo("/orders");
                }
            }
            else
            {
                _navigationManager.NavigateTo("/login");
            }

        }
        /// <summary>
        /// When called this method will return a list of all orders for the logged in authenticated user, asychronously from the server.
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderOverviewResponse>> GetAllOrdersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/orders");
            return result.Data;
        }

        public async Task<OrderDetailsResponse> GetOrderDetailsByIdAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/orders/{id}");
            return result.Data;
        }
    }
}
