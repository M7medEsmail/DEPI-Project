using DEPI_BusinessLogicLayer.IRepository;
using DEPI_BusinessLogicLayer.Repository;
using DEPI_DomainLayer.Context;
using DEPI_DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_BusinessLogicLayer.Services
{
    public class OrderService : GenericRepository<Order>, IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(StoreDbContext context) : base(context)
        {
            _orderRepository = new GenericRepository<Order>(context);
        }

        public async Task AddToCart(OrderItem orderItem , string userId)
        {
           
                // Otherwise, create a new order
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderItems = new List<OrderItem> { orderItem },
                    City = "null",
                    Country = "null",
                    Street = "null",
                    UserId = userId,

                };

                await _orderRepository.AddAsync(order);
            
        }

        public async Task<List<OrderItem>> GetCartItems(int orderId)
        {
            // Retrieve the order with its items
            var order = await GetOrderByIdAsync(orderId);
            return order?.OrderItems ?? new List<OrderItem>();
        }

        public async Task ClearCartAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.OrderItems.Clear();
                await _orderRepository.UpdateAsync(orderId, order);
            }
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            // Retrieve order with related order items
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<List<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders
                .Include(order => order.OrderItems) // Include OrderItems
                .Include(order => order.User) // Include User
                .Where(order => order.UserId == userId)
                .ToListAsync();
        }
    }
}
