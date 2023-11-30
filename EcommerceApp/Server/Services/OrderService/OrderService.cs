
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Place order function takes the user id guid to pass to the weebhook
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<bool>> PlaceOrderAsync(Guid userId)
        {
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

        public Task<ServiceResponse<List<OrderDetailsResponse>>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  When called, this method will return all the orders for all the users asynchronously from the server.
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<OrderDetailsResponse>>> GetAdminOrders()
        {
            var response = new ServiceResponse<List<OrderDetailsResponse>>();
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.DateCreated)
                .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                response.Success = false;
                response.Message = "No orders found. None may exist at this time.";
                response.Data = null;
                response.StatusCode = 404;
            }
            else
            {
                // Map the orders to the order details response which is a flattened version of the order.
                var orderDetailsResponse = orders.Select(o => new OrderDetailsResponse
                {
                    OrderDate = o.DateCreated,
                    TotalPrice = o.Total,
                    // Map the order items to the order details product response which is a flattened version of the order item.
                    Products = o.OrderItems.Select(oi => new OrderDetailsProductResponse
                    {
                        ProductId = oi.ProductId,
                        Name = oi.Product.Name,
                        ImageURI = oi.Product.ImageURI,
                        Quantity = oi.Quantity,
                        TotalPrice = oi.Price * oi.Quantity
                    }).ToList()
                }).ToList();

                response.Data = orderDetailsResponse;
                response.Success = true;
                response.Message = "Orders retrieved successfully.";
                response.StatusCode = 200;
            }

            return response;
        }



    }
}
