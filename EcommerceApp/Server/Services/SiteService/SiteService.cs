using AutoMapper;
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
    }
}
