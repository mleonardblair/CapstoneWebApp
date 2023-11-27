using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EcommerceApp.Client.Services.FavouriteService
{
    public class FavouriteService : IFavouriteService
    {
        private readonly HttpClient _httpClient;

        public FavouriteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteDto favouriteDto)
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
