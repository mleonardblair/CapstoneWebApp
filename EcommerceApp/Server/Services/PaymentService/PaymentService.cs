using EcommerceApp.Server.Helpers;
using EcommerceApp.Server.Services.AuthService;
using EcommerceApp.Server.Services.CartService;
using EcommerceApp.Shared.Models;
using Stripe;
using Stripe.Checkout;
using static System.Net.WebRequestMethods;

namespace EcommerceApp.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        const string secret = "whsec_n4T0DvXnmtPIeFjy6KXG0xmmvH5AbwTQ";
        public PaymentService(ICartService cartService, 
            IAuthService authService, 
            IOrderService orderService,
            IConfiguration configuration)
        {
            _configuration = configuration;

            // Use configuration to get the correct API key based on environment

            // TODO: Change to Live API key when ready to go live
            var stripeApi = _configuration.GetValue<string>(ConfigConstants.StripeTest_SecretKey);
            StripeConfiguration.ApiKey = "sk_test_51OG1LaBFTEKeFmtcR1FK2VXST0NqAtSORJ0LP9IRmC7fk1R2B9KfEyR3Mou1JkgUjCXHlfbOIPekExxf5lgwGXxS00u3Ua12br";

            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
        }
        public async Task<Session> CreateCheckoutSessionAsync()
        {
            // Get products from cart by userId
            var products = (await _cartService.GetCartItemsByUserId(_authService.GetUserId())).Data;
            // Create line items for Stripe
            var lineItems = new List<SessionLineItemOptions>();

            // Add products to line items
            foreach (var product in products)
            {
                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = product.Price * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Title,

                            Images = new List<string>() { product.ImageUrl }
                        }
                    },
                    Quantity = product.Quantity
                };
                lineItems.Add(lineItem);
            }


            /* var baseUrl = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "DEVELOPMENT"
                   ? "https://localhost:7194"
                   : "https://wasmcapstoneapp.azurewebsites.net";*/
            var baseUrl = "https://wasmcapstoneapp.azurewebsites.net";
            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string>{"CA"}
                },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment", // One Time Payment OTP - type of payment
                SuccessUrl = $"{baseUrl}/order-success",
                CancelUrl = $"{baseUrl}/cart"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }

        public async Task<ServiceResponse<bool>> FulfillOrderAsync(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
           // var webhookSecret = _configuration.GetValue<string>(ConfigConstants.StripeTest_WebhookSecret);
           var webhookSecret = secret;
            try {
                var stripeEvent = EventUtility.ConstructEvent(
                        json,
                        request.Headers["Stripe-Signature"],
                        webhookSecret
                    );
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetApplicationUserByEmail(session.CustomerEmail);
                    await _orderService.PlaceOrderAsync(user.Id);
                }
                return new ServiceResponse<bool> { Success = true };
            } catch(StripeException e) {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };
            }
        }
    }
}
