using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.OrderService
{
    public interface IOrderService
    {
        /// <summary>
        ///  For filtering in this context.
        /// </summary>
        public string SnackMessage { get; set; }
        public Severity Severity { get; set; }
        public event Action OnChange;
        public string Message { get; set; }
        // Admin
        public List<OrderDetailsResponse> AdminOrders { get; set; }
        // Customer
        public List<OrderDetailsResponse> Orders { get; set; }
        Task<string> PlaceOrderAsync();
        Task<List<OrderOverviewResponse>> GetAllOrdersAsync();
        Task<OrderDetailsResponse> GetOrderDetailsByIdAsync(Guid id);

        Task GetAdminOrders();
    }
}
