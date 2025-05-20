using Azure.Storage.Blobs;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcommerceApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private readonly ISiteService _siteService;
        private readonly ILogger<BlobController> _logger;

        public BlobController(
            BlobServiceClient blobServiceClient,
            IConfiguration configuration,
            ISiteService siteService,
            ILogger<BlobController> logger)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
            _siteService = siteService;
            _logger = logger;
        }

        [HttpPost("uploadlocal")]
        public async Task<ActionResult<ServiceResponse<BlobUploadResult>>> UploadLocal(IFormFile file)
        {
            var response = new ServiceResponse<BlobUploadResult>();

            try
            {
                _logger.LogInformation("Received local file upload: {FileName}, size: {Size}", file.FileName, file.Length);

                // Create uploads directory if it doesn't exist
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                _logger.LogInformation("Resolved uploads folder path: {Path}", uploadsFolder);
                Directory.CreateDirectory(uploadsFolder);
                    
                // Generate unique filename to prevent overwriting
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Create result with local URL
                var result = new BlobUploadResult
                {
                    FileUrl = $"/uploads/{uniqueFileName}",
                    IsUploaded = true
                };

                // Save to database using SiteService
                var dbResponse = await _siteService.AddGalleryImageAsync(result.FileUrl);

                if (dbResponse.Success)
                {
                    response.Success = true;
                    response.Message = "File uploaded and saved to database successfully!";
                    response.Data = result;
                    Console.WriteLine($"File uploaded successfully: {result.FileUrl}");
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Message = $"File uploaded but failed to save to database: {dbResponse.Message}";
                    response.Data = result;
                    Console.WriteLine($"Database save failed: {dbResponse.Message}");
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UploadLocal: {ex.Message}");
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<BlobUploadResult> UploadToBlob(IFormFile file)
        {
            var result = new BlobUploadResult();
            try
            {
                var productContainer = _configuration.GetSection("AzureBlobStorage:ProductAssetsContainer").Value;
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(productContainer);
                var blobClient = blobContainerClient.GetBlobClient(file.FileName);

                // Determine the content type from the file
                var contentType = file.ContentType;
            /*    // Here we ensure that if the file is a PNG image, we set the MIME type to 'image/apng'
                if (file.ContentType.ToLowerInvariant().Contains("png"))
                {
                    contentType = "image/apng";
                }
                // Set the MIME type for JPEG/JPG images
                else if (contentType.ToLowerInvariant().EndsWith("jpeg") || contentType.ToLowerInvariant().EndsWith("jpg"))
                {
                    contentType = "image/jpeg";
                }*/
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

       

        [HttpPost("upload")]
        public async Task<ActionResult<ServiceResponse<BlobUploadResult>>> Upload(IFormFile file)
        {
            var response = new ServiceResponse<BlobUploadResult>();
            // Upload the file to Azure Blob Storage.
            BlobUploadResult result = await UploadToBlob(file);
            if(result != null)
            {
                response.Success = true;
                response.Message = "File uploaded successfully!";
                response.Data = result;
                // Return the Azure Blob Storage URL and Success Code.
                return Ok(response);
            } else
            {
                response.Success = false;
                response.Message = "File upload failed!";
            }
            return BadRequest("An error occurred while uploading the file.");
        }


      

    }
    public class BlobUploadResult
    {
        public bool IsUploaded { get; set; }
        public string FileUrl { get; set; } = string.Empty;
    }


}
