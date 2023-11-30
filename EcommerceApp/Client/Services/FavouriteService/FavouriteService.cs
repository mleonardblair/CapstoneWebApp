using EcommerceApp.Client.Pages.Admin;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EcommerceApp.Client.Services.FavouriteService
{
    public class FavouriteService : IFavouriteService
    {
        private readonly HttpClient _httpClient;
        // This event will be used to notify subscribers that the products have changed
        // Define an event for favorite updates
        public event Action<Guid, bool> OnFavoriteUpdated;
        public List<FavouriteProductResponse> Favourites { get; set; } = new List<FavouriteProductResponse>();
        public FavouriteProductResponse Favourite { get; set; } = new FavouriteProductResponse();
        public FavouriteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ServiceResponse<List<FavouriteProductResponse>>> GetFavouritesByUserId(Guid? userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<FavouriteProductResponse>>>($"api/favourite/user/{userId}");
            return response;

        }
        public async Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteProductResponse favouriteDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/favourite", favouriteDto);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<FavouriteProductResponse>>();
        }

        public async Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<FavouriteProductResponse>>($"api/favourite/{id}");
            return response;
        }

        public async Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<FavouriteProductResponse>>>("api/favourite");
            return response;
        }

        public async Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favouriteDto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/favourite", favouriteDto);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<FavouriteProductResponse>>();
        }

        public async Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/favourite/{id}");
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
