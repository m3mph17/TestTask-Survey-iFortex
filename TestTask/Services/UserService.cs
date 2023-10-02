using Microsoft.EntityFrameworkCore;
using System;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    internal class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetUser()
        {
            var userWithMaxOrders = await _db.Users
                     .Include(u => u.Orders)
                     .OrderByDescending(u => u.Orders.Count)
                     .FirstOrDefaultAsync();

            if (userWithMaxOrders == null)
            {
                return null;
            }

            return userWithMaxOrders;
        }

        public async Task<List<User>> GetUsers()
        {
            var inactiveUsers = await _db.Users
                .Where(u => u.Status == Enums.UserStatus.Inactive)
                .ToListAsync();

            return inactiveUsers;
        }
    }
}
