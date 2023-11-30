using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApp.Client.Services.FavouriteService
{
    public interface IFavouriteService
    {
        // Define an event for favorite updates
        public event Action<Guid, bool> OnFavoriteUpdated;
        List<FavouriteProductResponse> Favourites { get; set; }
        FavouriteProductResponse Favourite { get; set; }
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetFavouritesByUserId(Guid? userId);
        Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteProductResponse favouriteDto);
        Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id);
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync();
        Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favouriteDto);
        Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id);
    }
}
