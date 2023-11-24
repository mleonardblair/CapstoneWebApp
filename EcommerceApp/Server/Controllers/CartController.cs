using Azure;
using EcommerceApp.Server.Models;
using EcommerceApp.Server.Services.CartService;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPut("update-quantity")]
        public async Task<ActionResult<ServiceResponse<CartItemDto>>> UpdateQuantity(CartItemDto cartItemDto)
        {
            return Ok(await _cartService.UpdateQuantity(cartItemDto));
        }

        [HttpGet("user")]
        public async Task<ActionResult<ServiceResponse<List<CartItemDto>>>> GetCartItemsForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userId, out Guid userIdGuid);
            return Ok(await _cartService.GetCartItemsByUserId(userIdGuid));
        }
        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<CartItemDto>>> AddCartItem(CartItemDto cartItemDto)
        {
            return Ok(await _cartService.AddCartItem(cartItemDto));
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateNewShoppingCart()
        {
            try
            {
                var response = await _cartService.CreateNewShoppingCartByUserId();
                return Ok(response);
            }
            catch
            {
                return BadRequest($"Couldn't retrieve shopping cart.");
            }
        }
        [HttpGet("items")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetDbCartProducts()
        {
            var result = await _cartService.GetDbCartProducts();
            return Ok(result);
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveCartItemById(Guid cartItemId)
        {
            return Ok(await _cartService.RemoveCartItemById(cartItemId));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse<CartDto>>> GetShoppingCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userId, out Guid userIdGuid);

            try
            {
                var response = await _cartService.GetShoppingCartByUserId(userIdGuid);
                return Ok(response);
            }
            catch
            {
                return BadRequest($"Couldn't retrieve shopping cart.");
            }
        }
        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartProducts(List<CartItemDto> cartItemDto)
        {
                var response = await _cartService.GetCartProducts(cartItemDto);
                return Ok(response);
            
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> StoreCartItems(List<CartItemDto> cartItemDto)
        {
            try
            {
                var response = await _cartService.StoreCartItems(cartItemDto);
                return Ok(response);
            } catch
            {
                return BadRequest($"Couldn't convert to shopping cart..");
            }
        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
        {
            try
            {
                var response = await _cartService.GetCartItemsCount();
                return Ok(response);
            }
            catch
            {
                return BadRequest($"Couldn't retrieve cart item count.");
            }
        }

    }

}

