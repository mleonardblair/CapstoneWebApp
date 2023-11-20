using Blazored.LocalStorage;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.CartService
{
    public class CartService : ICartService
    {
        public event Action OnChange;
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

        private readonly ILocalStorageService _localStorage;

        private readonly HttpClient _httpClient;
        public CartService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }
        public async Task AddToCart(CartItemDto cartItemDto)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if(cart == null)
            {
                cart = new List<CartItemDto>();
            }
            var sameItem = cart.Find(x => x.ProductId == cartItemDto.ProductId);
            if(sameItem == null)
            {
                cart.Add(cartItemDto);
            }else
            {
                sameItem.Quantity += cartItemDto.Quantity;
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

        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
            var cartProducts = 
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
            // Check if cartProducts is not null before accessing Data
            if (cartProducts != null && cartProducts.Data != null)
            {
                return cartProducts.Data;
            }
            else
            {
                // Handle the case where cartProducts is null
                // You might want to return an empty list or handle the error accordingly
                return new List<CartProductResponse>();
            }
        }

        public async Task RemoveProductFromCart(Guid productId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if(cart == null)
            {
                return;
            }
            var cartItem = cart.Find(x => x.ProductId == productId);
           
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke(); // Safely invoke the event, only if there's a subscriber
            }
            
        }

    }
}
