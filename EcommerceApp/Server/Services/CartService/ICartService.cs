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
    }
}
