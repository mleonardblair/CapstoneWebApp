using Azure.Storage.Blobs;
using EcommerceApp.Server.Services.SiteService;
using EcommerceApp.Shared.DTOs.PageDtos;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Server.Controllers
{
    /// <summary>
    /// This controller handles updates to site content from authorized users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ISiteService _siteService;

        public SiteController(IConfiguration configuration, BlobServiceClient blobServiceClient, ISiteService siteService)
        {
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
            _siteService = siteService;
        }
        // get 
        // This should be the gallery method to create a new gallery content item.
        [HttpPost]
        [Authorize(Roles = "Admin")] // Server-side enforcement of the role
        public async Task<BlobUploadResult> UploadBlobSiteContent(IFormFile file)
        {
            var result = new BlobUploadResult();
            try
            {
                var siteContentContainer = _configuration.GetSection("AzureBlobStorage:SiteModelAssetsContainer").Value;
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(siteContentContainer);
                var blobClient = blobContainerClient.GetBlobClient(file.FileName);

                // Determine the content type from the file
                var contentType = file.ContentType;

                using (var stream = file.OpenReadStream())
                {
                    // Set the content type for the blob
                    var blobHttpHeader = new Azure.Storage.Blobs.Models.BlobHttpHeaders
                    {
                        ContentType = contentType
                    };

                    await blobClient.UploadAsync(stream, new Azure.Storage.Blobs.Models.BlobUploadOptions
                    {
                        HttpHeaders = blobHttpHeader
                    });
                }

                result.FileUrl = blobClient.Uri.AbsoluteUri;
                result.IsUploaded = true;
            }
            catch (Exception ex)
            {
                result.IsUploaded = false;
                // Log the exception to your logging framework
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// When called this method will create a  
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("sitecontent")]
        [Authorize(Roles = "Admin")] // Server-side enforcement of the role
        public async Task<ActionResult<ServiceResponse<BlobUploadResult>>> UploadAsset(IFormFile file)
        {
            if (User.IsInRole("Admin"))
            {
                    // Process the file upload
                var response = new ServiceResponse<BlobUploadResult>();
                // Upload the file to Azure Blob Storage.
                BlobUploadResult result = await UploadBlobSiteContent(file);
                if (result != null)
                {
                    response.Success = true;
                    response.Message = "File uploaded successfully!";
                    response.Data = result;
                    // Return the Azure Blob Storage URL and Success Code.
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Message = "File upload failed!";
                }
                return BadRequest("An error occurred while uploading the file.");
                }
            else
            {
                return Forbid(); // Return a forbidden response
            }
        }


        /// <summary>
        /// This method will return the gallery data for testing and for display.
        /// </summary>
        /// <returns></returns>
        [HttpGet("gallery")]
        public async Task<ActionResult<ServiceResponse<GalleryDto>>> GetGallery()
        {
            var response = await _siteService.GetGalleryDataAsync();
            return Ok(response);
        }
    }

    
}
