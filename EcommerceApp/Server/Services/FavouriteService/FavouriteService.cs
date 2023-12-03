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
        private readonly IAuthService _authService;

        public FavouriteService(IMapper mapper, 
            AppDbContext context,
            IAuthService authService) {
            _mapper = mapper;
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<FavouriteProductResponse>> AddFavouriteAsync(FavouriteProductResponse favouriteDto)
        {
            var response = new ServiceResponse<FavouriteProductResponse>();
            try
            {
                // CHeck the current favourite doesnt already exist for the user ie, it doesn't have the same product id and application user id as any existing favourite
                var existingFavourite = await _context.Favourites.FirstOrDefaultAsync(f => f.ProductId == favouriteDto.ProductId && f.ApplicationUserId == favouriteDto.ApplicationUserId);

                // If the favourite already exists, return an error
                if (existingFavourite != null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Favourite already exists.";
                    response.StatusCode = 400;
                    return response;
                }
                var favouriteEntity = _mapper.Map<Favourite>(favouriteDto);
                // Add the favourite entity to the context
                await _context.Favourites.AddAsync(favouriteEntity);
                await _context.SaveChangesAsync();

                // After saving, fetch the product related to the favourite to build the response wrapper.
                var product = await _context.Products.FindAsync(favouriteDto.ProductId);
                if (product == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Product not found.";
                    response.StatusCode = 404;
                    return response;
                }

                // Create FavouriteProductResponse with both favourite and product details
                var favouriteProductResponse = new FavouriteProductResponse
                {
                    FavouriteId = favouriteEntity.Id,
                    ApplicationUserId = _authService.GetUserId(), // get the current logged in user. will always work due to authentication check on client.
                    DateAddedToFavourite = favouriteEntity.DateCreated,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    ProductPrice = product.Price,
                    ProductImageURI = product.ImageURI,
                    ProductStockQuantity = product.StockQuantity,
                };

                response.Data = favouriteProductResponse;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = 500;

            }

            return response;
        }


        public async Task<ServiceResponse<bool>> DeleteFavouriteAsync(Guid favouriteId)
        {
            var response = new ServiceResponse<bool>();
            var userId = _authService.GetUserId(); // Get the current logged-in user.

            // Directly find the favourite matching both favouriteId and userId
            var favourite = await _context.Favourites
                                          .FirstOrDefaultAsync(f => f.Id == favouriteId && f.ApplicationUserId == userId);

            // Check if the favourite was found
            if (favourite == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Favourite not found.",
                    StatusCode = 404
                };
            }
            else
            {
                try
                {
                    _context.Favourites.Remove(favourite);
                    await _context.SaveChangesAsync();

                    return new ServiceResponse<bool>
                    {
                        Data = true,
                        Success = true,
                        Message = "Favourite deleted successfully.",
                        StatusCode = 200
                    };
                }
                catch (Exception ex)
                {
                    return new ServiceResponse<bool>
                    {
                        Data = false,
                        Success = false,
                        Message = ex.Message,
                        StatusCode = 500 // Internal Server Error
                    };
                }
            }
        }


        public async Task<ServiceResponse<bool>> IsProductFavoritedByUser(Guid productId, Guid userId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                // Check if the product is already favourited by the user
                var favourite = await _context.Favourites.FirstOrDefaultAsync(f => f.ProductId == productId && f.ApplicationUserId == userId);

                // If the favourite exists, return true
                if (favourite != null)
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Product is favourited by user.";
                    response.StatusCode = 200;
                    return response;
                }
                else
                {
                    response.Data = false;
                    response.Success = true;
                    response.Message = "Product is not favourited by user.";
                    response.StatusCode = 200;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = 500;
                return response;
            }
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

        public async Task<ServiceResponse<List<FavouriteProductResponse>>> GetFavouritesByUserId(Guid userId)
        {
          // get the favourites that the user has added to their favourites list
          var favourites = await _context.Favourites
                .Include(f => f.Product)
                .Where(f => f.ApplicationUserId == userId)
                .ToListAsync();
            // if we fail to find any favourites, return an error
            if (favourites == null)
            {
                return new ServiceResponse<List<FavouriteProductResponse>>
                {
                    Data = null,
                    Success = false,
                    Message = "Favourites not found or there are none.",
                    StatusCode = 404
                };
            }
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
                };
            }));
            // return the favourites
            return new ServiceResponse<List<FavouriteProductResponse>>
            {
                Data = favouriteProductResponses.ToList(),
                Success = true,
                Message = "Favourites retrieved successfully.",
                StatusCode = 200
            };
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
                    ProductStockQuantity = favourite.Product.StockQuantity
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
