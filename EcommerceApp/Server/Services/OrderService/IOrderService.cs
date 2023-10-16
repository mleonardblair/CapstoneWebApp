using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDto>> CreateOrderAsync(OrderDto orderDto);
        Task<ServiceResponse<List<OrderDto>>> GetAllOrdersAsync();
        Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(Guid orderId);
        Task<ServiceResponse<OrderDto>> UpdateOrderByIdAsync(Guid orderId, OrderDto orderDto);
        Task<ServiceResponse<bool>> DeleteOrderByIdAsync(Guid orderId);
    }
}
