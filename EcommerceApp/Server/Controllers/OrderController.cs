using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EcommerceApp.Server.Services.OrderService;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

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

     
      /*  
       *  ONLY STRIPE CLI WEBHOOK CAN CALL THIS METHOD
       *  
       *  [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrderAsync()
        {
            var response = await _orderService.PlaceOrderAsync();
            return Ok(response);
        }*/

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
    }
}
