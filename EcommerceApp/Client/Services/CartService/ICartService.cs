using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;

        Task AddToCart(CartItemDto cartItemDto);
        Task<ServiceResponse<Guid>> CreateNewShoppingCart();
        Task<List<CartProductResponse>> GetCartProducts();
        Task RemoveProductFromCart(Guid productId);
        Task UpdateQuantity(CartProductResponse product);
        Task StoreCartItems(bool emptyLocalCart);
        Task<ServiceResponse<CartDto>> GetShoppingCartByUserId(Guid id);
       Task GetCartItemsCount();
        // Write a cartservice protocol method definition header for updating the quantity of the cart item using the server method
        Task UpdateQuantity(CartItemDto cartItemDto);
    }
}
