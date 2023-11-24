
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using EcommerceApp.Server.Services.CartService;
using EcommerceApp.Server.Services.AuthService;

namespace EcommerceApp.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;

        public OrderService(AppDbContext context, IMapper mapper, ICartService cartService, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _cartService = cartService;
            _authService = authService;
        }
        
        public async Task<ServiceResponse<OrderDto>> CreateOrderAsync(OrderDto orderDto)
        {
            var response = new ServiceResponse<OrderDto>();
            var order = _mapper.Map<Order>(orderDto);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<OrderDto>(order);
            response.Message = "Order successfully created.";
            return response;
        }

        public async Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(Guid orderId)
        {
            var response = new ServiceResponse<OrderDto>();
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            response.Data = _mapper.Map<OrderDto>(order);
            response.Message = "Order retrieved successfully.";
            return response;
        }

        public async Task<ServiceResponse<OrderDto>> UpdateOrderByIdAsync(Guid orderId, OrderDto orderDto)
        {
            var response = new ServiceResponse<OrderDto>();
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            _mapper.Map(orderDto, order);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<OrderDto>(order);
            response.Message = "Order updated successfully.";
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteOrderByIdAsync(Guid orderId)
        {
            var response = new ServiceResponse<bool>();
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "Order deleted successfully.";
            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrderAsync()
        {
            var userId = _authService.GetUserId();
            var cartItems = (await _cartService.GetCartItemsByUserId(userId)).Data;
            if (!cartItems.Any())
            {
                return new ServiceResponse<bool> { Data = false, Message = "Cart is empty." };
            }

            decimal subtotal = cartItems.Sum(product => product.Price * product.Quantity);
            decimal tax = subtotal * 0.13m;
            decimal total = subtotal + tax;

            var orderItems = new List<OrderItem>();
            cartItems.ForEach(product => orderItems
            .Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                Price = product.Price
            }));

            // Tax should be 13% of the subtotal
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                Date = DateTime.UtcNow,
                Subtotal = subtotal,
                Discount = 0,
                Tax = tax,
                Total = total,
                Status = OrderStatus.Processing,
                DateCreated = DateTime.UtcNow,
                OrderItems = orderItems

            };

            // Make the cart items order id equal to the new order.
            orderItems.ForEach(item => item.OrderId = order.Id);

            _context.OrderItems.AddRange(orderItems);

            _context.Orders.Add(order);

            // Retrieve the single ShoppingCart for the user
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            // Check if the cart exists
            if (cart != null)
            {
                // Remove the ShoppingCart
                _context.Carts.Remove(cart);
            }

             await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Order placed successfully." };
        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetAllOrdersAsync()
        {
            var response = new ServiceResponse<List<OrderOverviewResponse>>();
            var orders = await  _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o=> o.ApplicationUserId == _authService.GetUserId())
                .OrderByDescending(o => o.DateCreated)
                .ToListAsync();


            var orderResponse = new List<OrderOverviewResponse>();
            orders.ForEach(o=>orderResponse.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.Date,
                TotalPrice = o.Total,
                Product =  o.OrderItems.Count > 1 ? 
                    $"{o.OrderItems.First().Product.Name} and" + 
                    $"{o.OrderItems.Count - 1} more..." : 
                    o.OrderItems.First().Product.Name, 
                ProductImageUrl = o.OrderItems.First().Product.ImageURI,
            }));

            response.Data = orderResponse;

            return response;
        }
        /// <summary>
        /// Get the current user's order details asynchronously from the server.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetailsByIdAsync(Guid orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponse>();
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.ApplicationUserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.DateCreated)
                .FirstOrDefaultAsync();
            if(order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = order.DateCreated,
                TotalPrice = order.Total,
                Products = new List<OrderDetailsProductResponse>()
            };

            // Convert ICollection To List, then map the 
            foreach (var oi in order.OrderItems)
            {
                orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
                {
                    ProductId = oi.ProductId,
                    ImageURI = oi.Product.ImageURI,
                    Name = oi.Product.Name,
                    TotalPrice = oi.Price,
                    Quantity = oi.Quantity
                });
            }

            response.Data = orderDetailsResponse;

            return response;
        }
    }
}
