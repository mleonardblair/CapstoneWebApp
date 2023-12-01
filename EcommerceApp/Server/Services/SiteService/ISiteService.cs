using EcommerceApp.Shared.DTOs.PageDtos;

namespace EcommerceApp.Server.Services.SiteService
{
    public interface ISiteService
    {
        Task<GalleryDto> GetGalleryDataAsync();
    }
}
