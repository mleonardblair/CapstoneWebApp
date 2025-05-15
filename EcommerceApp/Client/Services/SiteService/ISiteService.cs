using EcommerceApp.Client.Shared;
using EcommerceApp.Shared.DTOs.PageDtos;
using Microsoft.AspNetCore.Components.Forms;

namespace EcommerceApp.Client.Services.SiteService
{
    public interface ISiteService
    {
        // Existing properties
        IList<IBrowserFile> files { get; set; }
        bool isAdmin { get; set; }
        string Role { get; set; }
        ReusableResultSnackbar Snackbar { get; set; }
        Severity SeverityMsgNotify { get; set; }
        string MessageNotify { get; set; }

        // Existing method
        Task<GalleryDto> GetGalleryAsync();

        // New method for uploading gallery images
        Task<bool> UploadGalleryImageAsync(IBrowserFile file);
    }
}
