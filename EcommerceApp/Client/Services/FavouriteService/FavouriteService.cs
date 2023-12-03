using EcommerceApp.Client.Pages.Admin;
using EcommerceApp.Client.Shared;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EcommerceApp.Client.Services.FavouriteService
{
    /// <summary>
    /// This class is used to manage the favorite products.
    /// </summary>
    public class FavouriteService : IFavouriteService
    {
        private readonly HttpClient _httpClient;
        // This event will be used to notify subscribers that the products have changed
        // Define an event for favorite updates
        public event Action<Guid, bool> OnFavoriteUpdated;
        public event Action FavouritesChanged;
        public ReusableResultSnackbar ReusableResultSnackbar { get; set; } = new ReusableResultSnackbar();
        public Guid ? UserId { get; set; }
        public string SnackMessage { get; set; } = "";
        public Severity Severity { get; set; } = Severity.Error;
      
        /// <summary>
        /// This property is used to store the list of favourites
        /// </summary>
        public List<FavouriteProductResponse> Favourites { get; set; } = new List<FavouriteProductResponse>();
        /// <summary>
        /// This property is used to store the a single favourite
        /// </summary>
        public FavouriteProductResponse Favourite { get; set; } = new FavouriteProductResponse();


        /// <summary>
        /// This constructor is used to inject the http client
        /// </summary>
        /// <param name="httpClient"></param>
        public FavouriteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// This method is used to get the list of favourites by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<FavouriteProductResponse>>> GetFavouritesByUserId(Guid? userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<FavouriteProductResponse>>>($"api/favourite/user/{userId}");
            return response;

        }

        /// <summary>
        /// This method is used to add a favourite
        /// </summary>
        /// <param name="favouriteDto"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteProductResponse favouriteDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/favourite", favouriteDto);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<FavouriteProductResponse>>();
        }

        /// <summary>
        /// This method is used to get a favourite by id
        /// </summary>
        /// <param name="id">The favourite id</param>
        /// <returns>the favourite response object</returns>
        public async Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<FavouriteProductResponse>>($"api/favourite/{id}");
            return response;
        }
        /// <summary>
        /// This method is used to get all favourites
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<FavouriteProductResponse>>>("api/favourite");
            return response;
        }

        /// <summary>
        /// This method is 
        /// </summary>
        /// <param name="favouriteDto"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favouriteDto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/favourite", favouriteDto);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<FavouriteProductResponse>>();
        }
        /// <summary>
        /// This method is used to delete a favourite
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/favourite/{id}");
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
        /// <summary>
        /// This method is used to check if a product is favorited by a user
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<bool>> IsProductFavoritedByUser(Guid productId, Guid userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<bool>>($"api/favourite/isfavorited/{productId}/{userId}");
            return response ?? new ServiceResponse<bool> { Data = false, Success = false, Message = "Error occurred" };
        }

        /// <summary>
        /// This method is used to indicate status of crud operation.
        /// </summary>
        public void ResetSnackbarMessage()
        {
            SnackMessage = "";
            Severity = Severity.Success; // Reset to a default severity
        }
    }
}
