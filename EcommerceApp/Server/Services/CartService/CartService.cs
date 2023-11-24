using AutoMapper;
using EcommerceApp.Server.Services.AuthService;
using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerceApp.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public readonly AppDbContext _context;

        public CartService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<ServiceResponse<Guid>> CreateNewShoppingCartByUserId()
        {
            // Create a new shopping cart for the user if the current shopping cart doesn't exist or is null.
            var response = new ServiceResponse<Guid>();
            try
            {
                // Get the user ID from the claims principal accessor method (the current user)
                var userId = _authService.GetUserId();
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        ApplicationUserId = userId,
                        CartItems = new List<CartItem>()
                    };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync(); // Use await here to await the SaveChangesAsync() method.

                    response = new ServiceResponse<Guid>
                    {
                        Data = cart.Id,
                        Message = "Created a new shopping cart for the user.",
                        Success = true
                    };
                }
                else
                {
                    response = new ServiceResponse<Guid>
                    {
                        Data = cart.Id,
                        Message = "Couldn't create a new shopping cart for the user.",
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                response = new ServiceResponse<Guid>
                {
                    Data = Guid.Empty,
                    Message = $"Couldn't create a new shopping cart for the user. {ex.Message}",
                    Success = false
                };
            }
            return response;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItemDto> cartItemsDto)
        {
            var response = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            foreach (var item in cartItemsDto)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .Select(p => new
                            {
                                p.Id,
                                p.Name,
                                p.Price,
                                p.ImageURI,
                                p.Description,
                                Tags = _context.ProductTags
                                            .Where(pt => pt.ProductId == p.Id)
                                            .Select(pt => pt.Tag.Name)
                                            .ToList()
                            })
                    .FirstOrDefaultAsync();

                if (product != null)
                {
                    var cartProductResponse = new CartProductResponse
                    {
                        Id = item.Id, // Use the CartItemDto ID
                        ProductId = product.Id,
                        Title = product.Name,
                        Price = product.Price,
                        ImageUrl = product.ImageURI,
                        Quantity = item.Quantity,
                        Tags = product.Tags
                    };

                    response.Data.Add(cartProductResponse);
                }
            }
            response.Message= $"Found ({response.Data.Count}) item(s) in the cart.";

            return response;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartItemsByUserId(Guid userId)
        {
            var response = new ServiceResponse<List<CartProductResponse>>();

            try
            {
                // First, get the ShoppingCart for the given user ID
                var shoppingCart = await _context.Carts
                    .FirstOrDefaultAsync(sc => sc.ApplicationUserId == userId);

                if (shoppingCart != null)
                {
                    // Get the CartItems from this ShoppingCart
                    var cartItems = await _context.CartItems
                        .Where(ci => ci.ShoppingCartId == shoppingCart.Id)
                        .Include(ci => ci.Product)
                        .ThenInclude(p => p.ProductTags)
                        .ToListAsync();

                    var cartProducts = new List<CartProductResponse>();

                    foreach (var cartItem in cartItems)
                    {
                        var product = cartItem.Product;
                        var cartProductResponse = new CartProductResponse
                        {
                            Id = cartItem.Id,
                            ShoppingCartId = cartItem.ShoppingCartId,
                            Title = product?.Name ?? string.Empty,
                            Price = product?.Price ?? 0m,
                            Description = product?.Description ?? string.Empty,
                            ImageUrl = product?.ImageURI ?? string.Empty,
                            Tags = product?.ProductTags.Select(pt => pt.Tag.Name).ToList() ?? new List<string>(),
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                            DateCreated = cartItem.DateCreated
                        };
                        cartProducts.Add(cartProductResponse);
                    }

                    response.Data = cartProducts;
                    response.Success = true;
                    response.Message = "Retrieved cart items with product details successfully.";
                }
                else
                {
                    response.Data = new List<CartProductResponse>();
                    response.Success = false;
                    response.Message = "No shopping cart found for the given user ID.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving cart items: {ex.Message}";
            }

            return response;
        }



        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(ShoppingCart shoppingCart)
        {
            var response = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            shoppingCart.CartItems.Select(async item =>
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        p.ImageURI,
                        p.Description,
                        Tags = _context.ProductTags
                                            .Where(pt => pt.ProductId == p.Id)
                                            .Select(pt => pt.Tag.Name)
                                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (product != null)
                {
                    var cartProductResponse = new CartProductResponse
                    {
                        Id = item.Id, // Use the CartItemDto ID
                        ProductId = product.Id,
                        Title = product.Name,
                        Price = product.Price,
                        ImageUrl = product.ImageURI,
                        Quantity = item.Quantity,
                        Tags = product.Tags
                    };

                    response.Data.Add(cartProductResponse);
                }

            });
        response.Message = $"Found ({response.Data.Count}) item(s) in the cart.";

            return response;
        }

        /*  public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItemDto> cartItems)
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
          }*/

        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItemDto> cartItemsDtos)
        {
            var userId = _authService.GetUserId(); // Get the user ID

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.ApplicationUserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var items = _mapper.Map<List<CartItem>>(cartItemsDtos);

            foreach (var cartItem in items)
            {
                cartItem.Id = Guid.NewGuid();
                cartItem.ShoppingCartId = cart.Id;
                cartItem.DateCreated = DateTime.UtcNow;
                _context.CartItems.Add(cartItem); // Add cart item to context
            }

            await _context.SaveChangesAsync(); // Save changes to the database

            var shoppingCart = await _context.Carts
                .Where(c => c.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            return await GetDbCartProducts();
        }
        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(ShoppingCart shoppingCart)
        {
            var userId = _authService.GetUserId(); // Get the user ID

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.ApplicationUserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    ApplicationUserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var items = shoppingCart.CartItems; // Get the cart items from the provided shopping cart

            foreach (var cartItem in items)
            {
                cartItem.Id = Guid.NewGuid();
                cartItem.ShoppingCartId = cart.Id;
                cartItem.DateCreated = DateTime.UtcNow;
                _context.CartItems.Add(cartItem); // Add cart item to context
            }

            await _context.SaveChangesAsync(); // Save changes to the database

            var updatedShoppingCart = await _context.Carts
                .Where(c => c.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            return await GetCartProducts(updatedShoppingCart);
        }


        /// <summary>
        /// Gets the number of cart items.
        /// </summary>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {

            // Get the user's shopping cart items
            var cartItems = await _context.Carts
                .Where(c => c.ApplicationUserId == _authService.GetUserId())
                .SelectMany(c => c.CartItems) // Flatten the collection of collections
                .ToListAsync();

            int count = cartItems.Count; // Count the total number of items

            if (count == 0)
            {
                return new ServiceResponse<int>
                {
                    Data = 0,
                    Message = "Couldn't find the cart items count based on the user id."
                };

            }else
            {
                return new ServiceResponse<int> 
                { 
                    Data = count,
                    Message = "Found the cart items count based on the user id."
                };
            }
        }

        public async Task<ServiceResponse<CartDto>> GetShoppingCartByUserId(Guid userId)
        {
            var response = new ServiceResponse<CartDto>();

            try
            {
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                        .ThenInclude(p => p.ProductTags)
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

                if (cart != null)
                {
                    // Manually map the ShoppingCart to ShoppingCartDto
                    var cartDto = new CartDto
                    {
                        Id = cart.Id,
                        ApplicationUserId = cart.ApplicationUserId.Value,
                        CartProducts = cart.CartItems.Select(ci => new CartProductResponse
                        {
                            Id = ci.Id,
                            ShoppingCartId = ci.ShoppingCartId,
                            Title = ci.Product.Name,
                            Price = ci.Product.Price,
                            Description = ci.Product.Description,
                            ImageUrl = ci.Product.ImageURI,
                            Tags = ci.Product.ProductTags.Select(pt => pt.Tag.Name).ToList(),
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity,
                            DateCreated = ci.DateCreated
                        }).ToList()
                    };

                    response = new ServiceResponse<CartDto>
                    {
                        Data = cartDto,
                        Message = "Retrieved the shopping cart by user id.",
                        Success = true
                    };
                }
                else
                {
                    response = new ServiceResponse<CartDto>
                    {
                        Data = null,
                        Message = "Couldn't retrieve the shopping cart by user id.",
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                response = new ServiceResponse<CartDto>
                {
                    Data = null,
                    Message = $"Couldn't retrieve the shopping cart by user id. {ex.Message}",
                    Success = false
                };
            }

            return response;
        }


        // Update the cart item where the cartitem corresponds the user id of the current user.
        public async Task<ServiceResponse<CartItemDto>> UpdateQuantity(CartItemDto cartItemDto)
        {
            var response = new ServiceResponse<CartItemDto>();
            try
            {
                var cartItem = _mapper.Map<CartItem>(cartItemDto);
                if (cartItem != null)
                {
                    _context.CartItems.Update(cartItem);
                    await _context.SaveChangesAsync();
                    response = new ServiceResponse<CartItemDto>
                    {
                        Data = cartItemDto,
                        Message = "Updated the cart item quantity for the user.",
                        Success = true
                    };
                }
                else
                {
                    response = new ServiceResponse<CartItemDto>
                    {
                        Data = cartItemDto,
                        Message = "Couldn't update the cart item quantity for the user.",
                        Success = false
                    };
                }

            }
            catch (Exception ex)
            {
                response = new ServiceResponse<CartItemDto>
                {
                    Data = null,
                    Message = $"Couldn't update the cart item quantity for the user. {ex.Message}",
                    Success = false
                };
            }

            return response;
        }

        public async Task<ServiceResponse<CartItemDto>> AddCartItem(CartItemDto cartItemDto)
        {
            var response = new ServiceResponse<CartItemDto>();

            try
            {
                // Assuming GetUserId() returns the current user's ID
                var userId = _authService.GetUserId();
                var shoppingCart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

                if (shoppingCart != null)
                {
                    var existingCartItem = shoppingCart.CartItems
                        .FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);

                    CartItem cartItemToUpdate;

                    if (existingCartItem == null)
                    {
                        // Create a new CartItem
                        var newCartItem = _mapper.Map<CartItem>(cartItemDto);
                        newCartItem.ShoppingCartId = shoppingCart.Id; // Set ShoppingCartId
                        newCartItem.ProductId = cartItemDto.ProductId; // Explicitly set ProductId
                        newCartItem.DateCreated = DateTime.UtcNow; // Set DateCreated
                        _context.CartItems.Add(newCartItem);
                        cartItemToUpdate = newCartItem;
                    }
                    else
                    {
                        // Update existing CartItem
                        existingCartItem.Quantity += 1; // Increment quantity
                        cartItemToUpdate = existingCartItem;
                    }

                    await _context.SaveChangesAsync();
                    var updatedCartDto = _mapper.Map<CartItemDto>(cartItemToUpdate);
                    response.Data = updatedCartDto;
                    response.Message = "Cart item added or updated successfully.";
                    response.Success = true;
                }
                else
                {
                    response.Data = null;
                    response.Message = "User's shopping cart not found.";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = $"Error in adding the cart item: {ex.Message}";
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveCartItem(Guid userId, Guid cartItemId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                // Find the ShoppingCart for the given user ID
                var shoppingCart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

                if (shoppingCart != null)
                {
                    // Find the CartItem in the ShoppingCart
                    var cartItem = shoppingCart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

                    if (cartItem != null)
                    {
                        // Remove the CartItem from the ShoppingCart
                        shoppingCart.CartItems.Remove(cartItem);
                        await _context.SaveChangesAsync();

                        response.Data = true;
                        response.Message = "Cart item removed successfully.";
                        response.Success = true;
                    }
                    else
                    {
                        response.Data = false;
                        response.Message = "Cart item not found in the shopping cart.";
                        response.Success = false;
                    }
                }
                else
                {
                    response.Data = false;
                    response.Message = "Shopping cart not found for the given user ID.";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = $"Error removing cart item: {ex.Message}";
                response.Success = false;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> RemoveCartItemById(Guid cartItemId)
        {
            // take the cartItemId and find the cart item in the shopping cart, then remove it from the shopping cart.
            var response = new ServiceResponse<bool>();
            try
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItemId);
                if (cartItem != null)
                {
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    response = new ServiceResponse<bool>
                    {
                        Data = true,
                        Message = "Removed the cart item from the shopping cart.",
                        Success = true
                    };
                }
                else
                {
                    response = new ServiceResponse<bool>
                    {
                        Data = false,
                        Message = "Couldn't remove the cart item from the shopping cart.",
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                response = new ServiceResponse<bool>
                {
                    Data = false,
                    Message = $"Couldn't remove the cart item from the shopping cart. {ex.Message}",
                    Success = false
                };
            }

            return response;
        }


        public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts()
        {
            var shoppingCart = await _context.Carts
                               .Include(c => c.CartItems)
                               .FirstOrDefaultAsync(c => c.ApplicationUserId == _authService.GetUserId());
        
            return await GetCartProducts(shoppingCart);
        }
    }
}
