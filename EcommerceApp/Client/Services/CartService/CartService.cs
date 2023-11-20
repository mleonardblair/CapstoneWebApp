using Blazored.LocalStorage;
using EcommerceApp.Shared.DTOs;

namespace EcommerceApp.Client.Services.CartService
{
    public class CartService : ICartService
    {
        public event Action OnChange;
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

        private readonly ILocalStorageService _localStorage;
        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task AddToCart(CartItemDto cartItemDto)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if(cart == null)
            {
                cart = new List<CartItemDto>();
            }
            cart.Add(cartItemDto);
            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task<List<CartItemDto>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cart == null)
            {
                cart = new List<CartItemDto>();
            }
            return cart;
        }
    }
}
