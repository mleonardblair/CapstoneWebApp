using Azure.Storage.Blobs;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        [HttpPost]
        public async Task<BlobUploadResult> UploadToBlob(IFormFile file)
        {
            var result = new BlobUploadResult();
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient("products");
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
