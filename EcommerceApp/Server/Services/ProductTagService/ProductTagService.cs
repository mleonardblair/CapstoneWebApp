using AutoMapper;
using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Services.ProductTagService
{
    public class ProductTagService : IProductTagService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductTagService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ProductTagDto>> CreateProductTagAsync(ProductTagDto productTag)
        {
            
                var response = new ServiceResponse<ProductTagDto>();

                try
                {
                    var producttag = _mapper.Map<ProductTag>(productTag);

                    // Manually set the Id if it's the default value
                    if (producttag.Id == Guid.Empty)
                    {
                        producttag.Id = Guid.NewGuid();
                    }

                // Add the new tag to the database
                await _context.ProductTags!.AddAsync(producttag);
                    await _context.SaveChangesAsync();

                    // Map the Tag entity back to a ProductTagDto
                    var createdProductTagDto = _mapper.Map<ProductTagDto>(producttag);

                    response.Data = createdProductTagDto;
                    response.Message = "Product Tag successfully created.";
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Data = null;
                    response.Message = $"An error occurred: {ex.Message} Inner Exception: {ex.InnerException?.Message}";
                    response.Success = false;
                }

                return response;
            
        }
    }
}
