using EcommerceApp.Shared.DTOs;

namespace EcommerceApp.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;

        Task AddToCart(CartItemDto cartItemDto);
        Task<List<CartItemDto>> GetCartItems();
    }
}
