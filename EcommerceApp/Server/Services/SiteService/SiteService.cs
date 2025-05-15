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

        public SiteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GalleryDto>> GetGalleryDataAsync()
        {
            var response = new ServiceResponse<GalleryDto>();
            var gallery = await _context.Galleries.FirstOrDefaultAsync();
            if (gallery != null)
            {
                var galleryDto = _mapper.Map<GalleryDto>(gallery);
                response.Data = galleryDto;
                response.Success = true;
                response.Message = "Gallery data retrieved successfully.";
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> AddGalleryImageAsync(string imageUrl)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var gallery = await _context.Galleries
                    .Include(g => g.GalleryImages)
                    .FirstOrDefaultAsync();

                if (gallery == null)
                {
                    gallery = new Gallery
                    {
                        Id = Guid.NewGuid(),
                        Title = "Gallery",
                        Subtitle = "Image Gallery",
                        DateModified = DateTime.UtcNow
                    };
                    _context.Galleries.Add(gallery);
                }

                gallery.GalleryImages.Add(new GalleryImage
                {
                    Id = Guid.NewGuid(),
                    ImageURI = imageUrl,
                    GalleryId = gallery.Id
                });

                gallery.DateModified = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Data = true;
                response.Message = "Gallery image added successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding gallery image: {ex.Message}");
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }

}