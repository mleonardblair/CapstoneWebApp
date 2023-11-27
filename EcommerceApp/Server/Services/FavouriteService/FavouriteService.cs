using AutoMapper;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Services.FavouriteService
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public FavouriteService(IMapper mapper, 
            AppDbContext context) {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteDto favouriteDto)
        {
            var response = new ServiceResponse<FavouriteProductResponse>();
            try
            {
                // Map the DTO to the Favourite entity
                var favouriteEntity = _mapper.Map<Favourite>(favouriteDto);

                // Add the favourite entity to the context
                await _context.Favourites.AddAsync(favouriteEntity);
                await _context.SaveChangesAsync();

                // After saving, fetch the product related to the favourite to build the response
                var product = await _context.Products.FindAsync(favouriteEntity.ProductId);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    return response;
                }

                // Create FavouriteProductResponse with both favourite and product details
                var favouriteProductResponse = new FavouriteProductResponse
                {
                    FavouriteId = favouriteEntity.Id,
                    DateAddedToFavourite = favouriteEntity.DateCreated,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    ProductPrice = product.Price,
                    ProductImageURI = product.ImageURI,
                    ProductStockQuantity = product.StockQuantity,
                    ProductImages = product.Images
                };

                response.Data = favouriteProductResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var favourite = await _context.Favourites.FindAsync(id);
                if (favourite == null)
                {
                    response.Success = false;
                    response.Message = "Favourite not found.";
                    return response;
                }

                _context.Favourites.Remove(favourite);
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<List<FavouriteProductResponse>>> GetAllFavouritesAsync()
        {
            var response = new ServiceResponse<List<FavouriteProductResponse>>();
            try
            {
                // Fetch the favourites and include necessary related data
                var favourites = await _context.Favourites.Include(f => f.Product).ToListAsync();

                // Use Task.WhenAll to await all asynchronous operations inside the Select
                var favouriteProductResponses = await Task.WhenAll(favourites.Select(async f =>
                {
                    // Perform any necessary additional asynchronous operations here
                    // For example, fetching more details about the product or related entities
                    // If no additional async operations are needed, this can be simplified

                    return new FavouriteProductResponse
                    {
                        FavouriteId = f.Id,
                        DateAddedToFavourite = f.DateCreated,
                        ProductId = f.Product.Id,
                        ProductName = f.Product.Name,
                        ProductDescription = f.Product.Description,
                        ProductPrice = f.Product.Price,
                        ProductImageURI = f.Product.ImageURI,
                        ProductStockQuantity = f.Product.StockQuantity,
                        ProductImages = f.Product.Images
                    };
                }));

                response.Data = favouriteProductResponses.ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<FavouriteProductResponse>> GetFavouriteByIdAsync(Guid id)
        {
            var response = new ServiceResponse<FavouriteProductResponse>();
            try
            {
                var favourite = await _context.Favourites.Include(f => f.Product).FirstOrDefaultAsync(f => f.Id == id);

                if (favourite == null)
                {
                    response.Success = false;
                    response.Message = "Favourite not found.";
                    return response;
                }

                var favouriteProductResponse = new FavouriteProductResponse
                {
                    FavouriteId = favourite.Id,
                    DateAddedToFavourite = favourite.DateCreated,
                    ProductId = favourite.Product.Id,
                    ProductName = favourite.Product.Name,
                    ProductDescription = favourite.Product.Description,
                    ProductPrice = favourite.Product.Price,
                    ProductImageURI = favourite.Product.ImageURI,
                    ProductStockQuantity = favourite.Product.StockQuantity,
                    ProductImages = favourite.Product.Images
                };

                response.Data = favouriteProductResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<FavouriteProductResponse>> UpdateFavouriteAsync(FavouriteDto favouriteDto)
        {
            var response = new ServiceResponse<FavouriteProductResponse>();
            try
            {
                var favourite = await _context.Favourites.Include(f => f.Product).FirstOrDefaultAsync(f => f.Id == favouriteDto.Id);

                if (favourite == null)
                {
                    response.Success = false;
                    response.Message = "Favourite not found.";
                    return response;
                }

                // Update the favourite with new values from favouriteDto
                // (Assuming favouriteDto contains fields that can be updated)

                await _context.SaveChangesAsync();

                var favouriteProductResponse = new FavouriteProductResponse
                {
                    FavouriteId = favourite.Id,
                    DateAddedToFavourite = favourite.DateCreated,
                    ProductId = favourite.Product.Id,
                    ProductName = favourite.Product.Name,
                    ProductDescription = favourite.Product.Description,
                    ProductPrice = favourite.Product.Price,
                    ProductImageURI = favourite.Product.ImageURI,
                    ProductStockQuantity = favourite.Product.StockQuantity,
                    ProductImages = favourite.Product.Images
                };

                response.Data = favouriteProductResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
