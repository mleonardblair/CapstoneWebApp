using Azure.Core;
using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.CartService
{
    public interface ICartService
    {
       
        Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItemDto> cartItems);
        Task<ServiceResponse<CartItemDto>> AddCartItem(CartItemDto cartItemDto);
        Task<ServiceResponse<bool>> RemoveCartItemById(Guid cartItemId);
        Task<ServiceResponse<bool>> RemoveCartItem(Guid userId, Guid cartItemId);
        Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItemDto> cartItems);
        Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(ShoppingCart shoppingCart);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts();
        Task<ServiceResponse<CartDto>> GetShoppingCartByUserId(Guid userId);
        Task<ServiceResponse<List<CartProductResponse>>> GetCartItemsByUserId(Guid userId);
        // Write a cartservice protocol method definition header for updating the quantity of the cart item.
        Task<ServiceResponse<CartItemDto>> UpdateQuantity(CartItemDto cartItemDto);
        Task<ServiceResponse<Guid>> CreateNewShoppingCartByUserId();

    }
}
