using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/tables")]
    public class TablesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TablesController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET ALL TABLES
        // =========================
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tables = await _context.DiningTables.ToListAsync();
            return Ok(tables);
        }

        // =========================
        // AVAILABLE TABLES
        // =========================
        [HttpGet("available")]
        public async Task<IActionResult> Available()
        {
            var tables = await _context.DiningTables
                .Where(t => t.Status == TableStatus.Available)
                .ToListAsync();

            return Ok(tables);
        }

        // =========================
        // GET ACTIVE ORDER FOR TABLE
        // =========================
        [HttpGet("table/{tableId}")]
        public async Task<IActionResult> GetByTable(int tableId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.MenuItem)
                .Where(o =>
                    o.TableId == tableId &&
                    o.Status != OrderStatus.Completed &&
                    o.Status != OrderStatus.Billed)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // =========================
        // COMPLETE TABLE (CORRECT LOGIC)
        // =========================
        // 
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTable(int id)
        {
            var table = await _context.DiningTables
                .FirstOrDefaultAsync(t => t.TableId == id);

            if (table == null)
                return NotFound("Table not found");

            var orders = await _context.Orders
                .Where(o =>
                    o.TableId == id &&
                    o.Status != OrderStatus.Completed &&
                    o.Status != OrderStatus.Billed)
                .ToListAsync();

            foreach (var order in orders)
            {
                order.Status = OrderStatus.Completed;
                order.PaidAmount = order.TotalAmount;
                order.PaidDate = DateTime.UtcNow;
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b =>
                    b.TableId == id &&
                    b.Status != BookingStatus.Completed &&
                    b.Status != BookingStatus.Cancelled);

            if (booking != null)
                booking.Status = BookingStatus.Completed;

            table.Status = TableStatus.Available;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Table completed successfully"
            });
        }
        [HttpPut("{id}/maintenance")]
        public async Task<IActionResult> SetMaintenance(int id)
        {
            var table = await _context.DiningTables.FindAsync(id);

            if (table == null)
                return NotFound();

            table.Status = TableStatus.Maintenance;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Table set to maintenance" });
        }
    }
}