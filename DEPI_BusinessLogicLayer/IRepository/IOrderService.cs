using DEPI_DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_BusinessLogicLayer.IRepository
{
    public interface IOrderService
    {
        Task AddToCart(OrderItem orderItem, string userId);
        Task<List<OrderItem>> GetCartItems(int orderId);
        Task ClearCartAsync(int orderId);
        Task<List<Order>> GetOrdersByUserId(string userId);
    }
}
