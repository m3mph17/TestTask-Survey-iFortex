using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    internal class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Order> GetOrder()
        {
            var order = await _db.Orders
                        .OrderByDescending(o => o.Price * o.Quantity)
                        .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _db.Orders
                .Where(o => o.Quantity > 10)
                .ToListAsync();

            return orders;
        }
    }
}
