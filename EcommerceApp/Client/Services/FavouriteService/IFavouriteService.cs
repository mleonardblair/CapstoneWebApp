using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApp.Client.Services.FavouriteService
{
    public interface IFavouriteService
    {
        Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteDto favouriteDto);
        Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id);
        Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync();
        Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favouriteDto);
        Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id);
    }
}
