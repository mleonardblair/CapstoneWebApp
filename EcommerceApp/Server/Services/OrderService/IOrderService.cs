using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrderAsync();
        Task<ServiceResponse<OrderDto>> CreateOrderAsync(OrderDto orderDto);
        Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(Guid orderId);
        Task<ServiceResponse<OrderDto>> UpdateOrderByIdAsync(Guid orderId, OrderDto orderDto);
        Task<ServiceResponse<bool>> DeleteOrderByIdAsync(Guid orderId);
        Task<ServiceResponse<List<OrderOverviewResponse>>> GetAllOrdersAsync();
        Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetailsByIdAsync(Guid orderId);
    }
}
