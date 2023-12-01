using EcommerceApp.Client.Shared;
using EcommerceApp.Shared.DTOs.PageDtos;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.SiteService
{
    public class SiteService : ISiteService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public SiteService(HttpClient httpClient,
            IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public bool isAdmin { get; set; }
        public string Role { get; set; }
        // Store the files from the input element, then we can update when save pressed
        public IList<IBrowserFile> files { get; set; } = new List<IBrowserFile>();
        public ReusableResultSnackbar Snackbar { get; set; }
        public Severity SeverityMsgNotify { get; set; }
        public string MessageNotify { get; set; }

        public async Task<GalleryDto> GetGalleryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<GalleryDto>("api/site/gallery");
            if (response != null)
                return response;
            throw new InvalidOperationException("Failed to load gallery.");
        }
    }
}
