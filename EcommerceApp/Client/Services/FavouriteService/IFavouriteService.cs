using EcommerceApp.Client.Shared;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceApp.Client.Services.FavouriteService
{
    /// <summary>
    /// This interface is used to define methods for favourite service.
    /// </summary>
    public interface IFavouriteService
    {
        // Define an event for favorite updates
        public event Action FavouritesChanged;
        public event Action<Guid, bool> OnFavoriteUpdated;
        public Guid? UserId { get; set; }
        public List<FavouriteProductResponse> Favourites { get; set; }
        FavouriteProductResponse Favourite { get; set; }
        public ReusableResultSnackbar ReusableResultSnackbar { get; set; }
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetFavouritesByUserId(Guid? userId);
        Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteProductResponse favouriteDto);
        public string SnackMessage { get; set; }
        void ResetSnackbarMessage();
        public Severity Severity { get; set; }
        /// <summary>
        /// This method is used to get a favourite by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id);

        /// <summary>
        /// this method is used to get all favourites.
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync();

        /// <summary>
        /// This method is used to update a favourite.
        /// </summary>
        /// <param name="favouriteDto"></param>
        /// <returns></returns>
        Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favouriteDto);

        /// <summary>
        /// This method is used to delete a favourite.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id);

        /// <summary>
        /// This method is used to check if a product is favorited by a user.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResponse<bool>> IsProductFavoritedByUser(Guid productId, Guid userId);

    }
}
