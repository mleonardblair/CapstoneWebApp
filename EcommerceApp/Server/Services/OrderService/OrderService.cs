using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<ServiceResponse<List<OrderDto>>> GetAllOrdersAsync()
        {
            var response = new ServiceResponse<List<OrderDto>>();
            var orders = await _context.Orders.ToListAsync();

            response.Data = _mapper.Map<List<OrderDto>>(orders);
            response.Message = "Retrieved all orders.";
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
    }
}
