using EcommerceApp.Shared.Models;
using Stripe.Checkout;

namespace EcommerceApp.Server.Services.PaymentService
{
    public interface IPaymentService
    {
        
        Task<Session> CreateCheckoutSessionAsync();
        Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request);
    }
}
