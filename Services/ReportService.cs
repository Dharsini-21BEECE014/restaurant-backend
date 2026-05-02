using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        // 1. Revenue by category
        public async Task<object> GetRevenue()
        {
            var data = await _context.OrderItems
                .Where(i => i.Order.Status == OrderStatus.Billed)
                .GroupBy(i => i.MenuItem.MenuCategory.Name)
                .Select(g => new
                {
                    Category = g.Key,
                    Revenue = g.Sum(x => x.TotalPrice)
                }).ToListAsync();

            return data;
        }

        // 2. Top 5 items
        public async Task<object> TopItems()
        {
            return await _context.OrderItems
                .GroupBy(i => i.MenuItem.Name)
                .Select(g => new
                {
                    Item = g.Key,
                    Qty = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.Qty)
                .Take(5)
                .ToListAsync();
        }

        // 3. Tables > 2 hours
        public async Task<object> LongOccupied()
        {
            return await _context.Bookings
                .Where(b => b.Status == BookingStatus.Seated &&
                       b.BookingDate.AddHours(2) < DateTime.Now)
                .ToListAsync();
        }
    }
}