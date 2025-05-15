using EcommerceApp.Shared.DTOs.PageDtos;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.SiteService
{
    public interface ISiteService
    {
        // Existing method
        Task<ServiceResponse<GalleryDto>> GetGalleryDataAsync();

        // New method for adding gallery images to database
        Task<ServiceResponse<bool>> AddGalleryImageAsync(string imageUrl);
    }
}
