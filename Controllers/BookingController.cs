using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;
using RestaurantAPI.DTOs;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE BOOKING
        // [HttpPost]
        // public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
        // {
        //     var table = await _context.DiningTables.FindAsync(request.TableId);

        //     if (table == null)
        //         return NotFound("Table not found");

        //     if (table.Status != TableStatus.Available)
        //         return BadRequest("Table not available");

        //     if (request.GuestCount <= 0 || request.GuestCount > table.Capacity)
        //         return BadRequest("Invalid guest count");
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
        {
            Console.WriteLine("🔥 CREATE BOOKING HIT");

            Console.WriteLine($"Name: {request.CustomerName}");
            Console.WriteLine($"Phone: {request.CustomerPhone}");
            Console.WriteLine($"TableId: {request.TableId}");
            Console.WriteLine($"Guests: {request.GuestCount}");

            var table = await _context.DiningTables.FindAsync(request.TableId);

            if (table == null)
            {
                Console.WriteLine("❌ Table not found");
                return NotFound("Table not found");
            }

            Console.WriteLine("✅ Table found: " + table.TableNumber);

            if (table.Status != TableStatus.Available)
            {
                Console.WriteLine("❌ Table not available");
                return BadRequest("Table not available");
            }

            if (request.GuestCount > table.Capacity)
            {
                Console.WriteLine("❌ Invalid guest count");
                return BadRequest("Invalid guest count");
            }

            Console.WriteLine("🚀 Creating booking...");
            if (request.BookingDate < DateTime.Now)
                return BadRequest("Booking date must be future");

            // 🔥 FIXED BOOKING NUMBER
            var lastBooking = await _context.Bookings
                .OrderByDescending(b => b.BookingId)
                .Select(b => b.BookingNumber)
                .FirstOrDefaultAsync();

            int next = 1;
            if (!string.IsNullOrEmpty(lastBooking))
                next = int.Parse(lastBooking.Replace("BKG-", "")) + 1;

            string bookingNumber = $"BKG-{next:D4}";

            var booking = new Booking
            {
                BookingNumber = bookingNumber,
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                GuestCount = request.GuestCount,
                TableId = request.TableId,
                BookingDate = request.BookingDate,
                Status = BookingStatus.Confirmed,
                CreatedDate = DateTime.Now
            };

            table.Status = TableStatus.Reserved;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            Console.WriteLine("🎉 Booking saved successfully");
            return Ok(new
            {
                booking.BookingId,
                booking.BookingNumber,
                booking.CustomerName,
                booking.CustomerPhone,
                booking.GuestCount,
                booking.Status
            });
        }

        // GET BOOKINGS (FIXED RESPONSE)
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Table)
                .ToListAsync();

            return Ok(bookings.Select(b => new
            {
                b.BookingId,
                b.BookingNumber,
                b.CustomerName,
                b.CustomerPhone,
                b.GuestCount,
                Status = b.Status.ToString(),
                TableId = b.TableId,
                TableNumber = b.Table.TableNumber,
                Capacity = b.Table.Capacity
            }));
        }

        // GET BY ID (DETAIL VIEW)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Table)
                .Include(b => b.Orders)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
                return NotFound();

            return Ok(new
            {
                booking.BookingId,
                booking.BookingNumber,
                booking.CustomerName,
                booking.CustomerPhone,
                booking.GuestCount,
                booking.Status,
                TableNumber = booking.Table.TableNumber,
                Capacity = booking.Table.Capacity,
                Orders = booking.Orders.Select(o => new
                {
                    o.OrderNumber,
                    o.TotalAmount,
                    o.Status
                })
            });
        }

        // SEAT BOOKING
        [HttpPut("{id}/seat")]
        public async Task<IActionResult> SeatBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            if (booking.Status == BookingStatus.Seated)
            {
                return Ok(booking); // already seated → do nothing
            }

            if (booking.Status != BookingStatus.Confirmed)
                return BadRequest("Only confirmed bookings can be seated");

            var table = await _context.DiningTables.FindAsync(booking.TableId);

            if (table != null)
                table.Status = TableStatus.Occupied;

            booking.Status = BookingStatus.Seated;

            await _context.SaveChangesAsync();
            return Ok(booking);
        }

        // CANCEL BOOKING
        [HttpPut("{bookingId}/cancel")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
                return NotFound("Booking not found");

            // ❌ prevent cancel if already seated/completed
            if (booking.Status == BookingStatus.Seated)
                return BadRequest("Cannot cancel after being seated");

            if (booking.Status == BookingStatus.Completed)
                return BadRequest("Already completed");

            // ✅ cancel booking
            booking.Status = BookingStatus.Cancelled;

            // ✅ free table
            var table = await _context.DiningTables
                .FirstOrDefaultAsync(t => t.TableId == booking.TableId);

            if (table != null)
                table.Status = TableStatus.Available;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking cancelled successfully" });
        }
    }
}