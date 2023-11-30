using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EcommerceApp.Server.Services.OrderService;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceApp.Server.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

     
 
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrderAsync(Guid id)
        {
            var response = await _orderService.PlaceOrderAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponse>>>> GetAllOrdersAsync()
        {
            var response = await _orderService.GetAllOrdersAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<OrderDto>>> GetOrderDetailsByIdAsync(Guid id)
        {
            var response = await _orderService.GetOrderDetailsByIdAsync(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<OrderDto>>> UpdateOrderByIdAsync(Guid id, OrderDto orderDto)
        {
            var response = await _orderService.UpdateOrderByIdAsync(id, orderDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteOrderByIdAsync(Guid id)
        {
            var response = await _orderService.DeleteOrderByIdAsync(id);
            return Ok(response);
        }


        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<OrderDetailsResponse>>>> GetAdminOrders()
        {
            var result = await _orderService.GetAdminOrders();
            return Ok(result);
        }
    }
}
