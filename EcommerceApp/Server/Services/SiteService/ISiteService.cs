using EcommerceApp.Shared.DTOs.PageDtos;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.SiteService
{
    public interface ISiteService
    {
        Task<ServiceResponse<GalleryDto>> GetGalleryDataAsync();
    }
}
