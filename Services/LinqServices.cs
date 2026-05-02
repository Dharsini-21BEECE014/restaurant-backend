using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class LinqService
    {
        private readonly AppDbContext _context;

        public LinqService(AppDbContext context)
        {
            _context = context;
        }

        // 1. Revenue by category (current month)
        public async Task<object> GetCategoryRevenue()
        {
            var data = await _context.Orders
                .Where(o => o.Status == OrderStatus.Billed &&
                            o.OrderDate.Month == DateTime.Now.Month)
                .SelectMany(o => o.OrderItems)
                .Include(i => i.MenuItem)
                .ThenInclude(m => m.MenuCategory)
                .GroupBy(i => i.MenuItem.MenuCategory.Name)
                .Select(g => new
                {
                    Category = g.Key,
                    Revenue = g.Sum(x => x.TotalPrice)
                })
                .ToListAsync();

            return data;
        }

        // 2. Top 5 menu items
        public async Task<object> GetTopItems()
        {
            var data = await _context.OrderItems
                .Include(i => i.MenuItem)
                .GroupBy(i => i.MenuItem.Name)
                .Select(g => new
                {
                    Item = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToListAsync();

            return data;
        }

        // 3. Tables occupied > 2 hours
        public async Task<object> GetLongOccupiedTables()
        {
            var data = await _context.Bookings
                .Include(b => b.Table)
                .Where(b =>
                    b.Status == BookingStatus.Seated &&
                    b.BookingDate.AddHours(2) < DateTime.Now)
                .Select(b => new
                {
                    b.Table.TableNumber,
                    b.BookingDate,
                    Hours = EF.Functions.DateDiffHour(b.BookingDate, DateTime.Now)
                })
                .ToListAsync();

            return data;
        }
    }
}