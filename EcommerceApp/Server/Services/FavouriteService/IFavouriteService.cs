using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.FavouriteService
{
    
    public interface IFavouriteService
    {
        Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteDto favourite);
        Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id);
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync();
        Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favourite);
        Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id);
    }
}

