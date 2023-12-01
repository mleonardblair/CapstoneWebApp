using AutoMapper;
using EcommerceApp.Shared.DTOs.PageDtos;
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
        public async Task<GalleryDto> GetGalleryDataAsync()
        {
            // Simulate fetching data from a database or other source
            var galleryDto = new GalleryDto
            {
                Title = "Gallery",
                Subtitle = "This is where I host work and whatnot.",
                GalleryImages = new List<string>
            {
                "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath1.jpg",
                "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath2.jpg",
                "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath3.jpg",
                "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath4.jpg",
                "https://ecommerceblobstorage.blob.core.windows.net/sitecontent/wreath5.jpg",
            },
                DateModified = DateTime.UtcNow
            };

            return galleryDto;


/*            var galleryEntity = await _context.Galleries.FirstOrDefaultAsync();
            return _mapper.Map<GalleryDto>(galleryEntity);*/
        }
    }
}
