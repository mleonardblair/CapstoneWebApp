using AutoMapper;
using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        public readonly AppDbContext _context;
        public CartService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItemDto> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>()
            {
                Data = new List<CartProductResponse>()
            };
            foreach(var item in cartItems)
            {
                // Fetch the product from the database only once
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        p.ImageURI,
                        p.Description,
                        Tags = _context.ProductTags
                            .Where(pt => pt.ProductId == p.Id)
                            .Select(pt => pt.Tag != null ? pt.Tag.Name : null) // Handle null without null-conditional operator
                            .ToList()
                    })
                    .FirstOrDefaultAsync();




                // Check if the product exists in the database
                if (product != null)
                {
                    // Create a new CartProductResponse object using the fetched data
                    var cartItemDto = new CartProductResponse
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        Title = product.Name,
                        Price = product.Price,
                        ImageUrl = product.ImageURI,
                        Quantity = item.Quantity,
                        Tags = product.Tags
                    };

                    // Add the cartItemDto to the result
                    result.Data.Add(cartItemDto);
                }
            }
            return result;
        }

      
    }
}
