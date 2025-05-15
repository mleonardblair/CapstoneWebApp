using EcommerceApp.Client.Shared;
using EcommerceApp.Shared.DTOs.PageDtos;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace EcommerceApp.Client.Services.SiteService
{
    public class SiteService : ISiteService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public SiteService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public bool isAdmin { get; set; }
        public string Role { get; set; }
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

        public async Task<bool> UploadGalleryImageAsync(IBrowserFile file)
        {
            try
            {
                Console.WriteLine($"Uploading file: {file.Name}, size: {file.Size}");

                var buffer = new byte[file.Size];
                await file.OpenReadStream(5120000).ReadAsync(buffer);

                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new ByteArrayContent(buffer), "file", file.Name);

                // Use the local upload endpoint
                var response = await _httpClient.PostAsync("api/blob/uploadlocal", content);

                if (response.IsSuccessStatusCode)
                {
                    var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<BlobUploadResult>>();
                    Console.WriteLine($"Upload response: {serviceResponse?.Success}, Message: {serviceResponse?.Message}");
                    return serviceResponse?.Success ?? false;
                }

                Console.WriteLine($"Upload failed with status: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UploadGalleryImageAsync: {ex.Message}");
                return false;
            }
        }
    }
}