using Azure;
using Blazored.LocalStorage;
using EcommerceApp.Client.Services.AuthService;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Claims;

namespace EcommerceApp.Client.Services.CartService
{
    public class CartService : ICartService
    {
        // Determine if we have to merge the data from the local storage with the data from the server or get the data from the server

        public event Action OnChange;
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public CartDto ShoppingCart { get; set; } = new CartDto();
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public CartService(ILocalStorageService localStorage, HttpClient httpClient, IAuthService authService)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _authService = authService;
        }
        public async Task<ServiceResponse<Guid>> CreateNewShoppingCart()
        {
            var id = await _httpClient.GetFromJsonAsync<ServiceResponse<Guid>>("api/cart");
            return id;
        }
        public async Task AddToCart(CartItemDto cartItemDto)
        {
            // Check if the user is authenticated before adding the item to the cart in the local storage
            if (await _authService.IsUserAuthenticated())
            {
                // Add to shopping cart on the server if logged in.
                var response = await _httpClient.PostAsJsonAsync("api/cart/add", cartItemDto);
                Console.WriteLine("User is authenticated");
            }
            else
            {
                Console.WriteLine("User is NOT authenticated");
                var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                if (cart == null)
                {
                    cart = new List<CartItemDto>();
                }
                var sameItem = cart.Find(x => x.ProductId == cartItemDto.ProductId);
                if (sameItem == null)
                {
                    cart.Add(cartItemDto);
                }
                else
                {
                    sameItem.Quantity += cartItemDto.Quantity;
                }

            await _localStorage.SetItemAsync("cart", cart);
            }
            await GetCartItemsCount(); // Invoke is already within this method so unneccessary to add after.
        }
        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            if(await _authService.IsUserAuthenticated())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/cart/user" );
                return response.Data;
              }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                if (cartItems == null)
                    return new List<CartProductResponse>();
                var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
                var cartProducts =
                    await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
                // Check if cartProducts is not null before accessing Data
                return cartProducts.Data;
            }
            
        
        }

        public async Task RemoveProductFromCart(Guid productId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cart == null)
            {
                return;
            }
            var cartItem = cart.Find(x => x.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                await GetCartItemsCount();
                OnChange.Invoke(); // Safely invoke the event, only if there's a subscriber
            }

        }

        public async Task UpdateQuantity(CartProductResponse product)
        {
            // TODO: Implement this method to update the quantity of a product in the cart in the local storage
            if(await _authService.IsUserAuthenticated())
            {
                // Update the quantity of the cart item on the server if logged in.
                var response = await _httpClient.PutAsJsonAsync("api/cart/update-quantity", product);
                Console.WriteLine("Update cart item quantity.");
            } else
            {
                Console.WriteLine("User is NOT authenticated");
            }
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cart == null)
            {
                return;
            }
            var cartItem = cart.Find(x => x.ProductId == product.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
        }
        /// <summary>
        /// When called this method will store the cart items from the local storage to the server.
        /// </summary>
        /// <param name="emptyLocalCart"></param>
        /// <returns></returns>
        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (localCart == null)
            {
                return; // DO nothing if no items in the cart. nothing to add to the server.
            }

            // Check if the user is authenticated before adding the item to the cart in the local storage
           await _httpClient.PostAsJsonAsync("api/cart", localCart);
            // If the cart items were successfully stored on the server, empty the local cart.
            if (emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");
            }

        }
        public async Task GetCartItemsCount()
        {

            if (await _authService.IsUserAuthenticated())
            {
                // For authenticated users, fetch the count from the server.
                // create a new shopping cart on the server if it doesn't exist for the current user.
                var response = await CreateNewShoppingCart();
                Console.WriteLine($" Shopping Cart Id: {response.Data}");
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
                var count = result.Data;
                await _localStorage.SetItemAsync<int>("cartItemsCount", count);
            }
            else
            {
                // For unauthenticated users, calculate the count based on local storage.
                var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                var count = cart?.Count ?? 0;
                await _localStorage.SetItemAsync<int>("cartItemsCount", count);
            }
            OnChange.Invoke();
        }

        public async Task<ServiceResponse<CartDto>> GetShoppingCartByUserId(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<CartDto>>
                ($"api/cart/{id}");
            if (response != null && response?.Data != null)
            {
                return response;
            }
            else
            {
                return new ServiceResponse<CartDto>
                {
                    Success = false,
                    Message = "Something went wrong during retrieving the shopping cart."
                };
            }
        }

        public async Task UpdateQuantity(CartItemDto cartItemDto)
        {
            if (await _authService.IsUserAuthenticated())
            {
                // Update the quantity of the cart item on the server if logged in.
                var response = await _httpClient.PutAsJsonAsync("api/cart/update-quantity", cartItemDto);
                Console.WriteLine("Update cart item quantity.");
            }
            else
            {
                Console.WriteLine("User is NOT authenticated");
                // Update the quantity of the cart item in the local storage
                var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                if (cart == null)
                {
                    return;
                }
                var cartItem = cart.Find(x => x.ProductId == cartItemDto.ProductId);
                if (cartItem != null)
                {
                    cartItem.Quantity = cartItemDto.Quantity;
                    await _localStorage.SetItemAsync("cart", cart);
                    OnChange?.Invoke();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
