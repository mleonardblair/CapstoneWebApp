using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.FavouriteService
{
    
    public interface IFavouriteService
    {
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetFavouritesByUserId(Guid userId);
        Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteProductResponse favourite);
        Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id);
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync();
        Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favourite);
        Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id);
    }
}

