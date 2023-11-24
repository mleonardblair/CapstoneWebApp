using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task PlaceOrderAsync();
        Task<List<OrderOverviewResponse>> GetAllOrdersAsync();
        Task<OrderDetailsResponse> GetOrderDetailsByIdAsync(Guid id);
    }
}
