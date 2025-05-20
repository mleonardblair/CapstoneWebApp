using AutoMapper;
using EcommerceApp.Server.Models.PageModels;
using EcommerceApp.Shared.DTOs.PageDtos;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Services.SiteService
{
    public class SiteService : ISiteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SiteService> _logger;

        public SiteService(AppDbContext context, IMapper mapper, ILogger<SiteService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<GalleryDto>> GetGalleryDataAsync()
        {
            var response = new ServiceResponse<GalleryDto>();
            var gallery = await _context.Galleries
    .Include(g => g.GalleryImages)
    .FirstOrDefaultAsync();

            if (gallery != null)
            {
                var galleryDto = _mapper.Map<GalleryDto>(gallery);
                response.Data = galleryDto;
                response.Success = true;
                response.Message = "Gallery data retrieved successfully.";
            }
            return response;
        }
        public async Task<ServiceResponse<string>> AddGalleryImageAsync(string fileUrl)
        {
            var response = new ServiceResponse<string>();

            // 🔍 Step 1: Get the default gallery
            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(); // or use a fixed ID

            if (gallery == null)
            {
                response.Success = false;
                response.Message = "Default gallery not found.";
                return response;
            }

            // 🖼️ Step 2: Create the new image and attach to gallery
            var newImage = new GalleryImage
            {
                Id = Guid.NewGuid(),
                ImageURI = fileUrl,
                GalleryId = gallery.Id
            };
            _logger.BeginScope("Adding new image to gallery: {ImageURI}", fileUrl);
            _context.GalleryImages.Add(newImage);
            gallery.DateModified = DateTime.UtcNow;

            // 💾 Step 3: Save
            try
            {
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Image added to gallery.";
                response.Data = newImage.Id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save gallery image");
                response.Success = false;
                response.Message = "Failed to save image to gallery.";
            }

            return response;
        }

    }

}