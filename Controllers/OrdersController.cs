using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;
using RestaurantAPI.DTOs;


namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE ORDER
        // =========================
        // [HttpPost]
        // public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        // {
        //     if (request == null || request.Items == null || !request.Items.Any())
        //         return BadRequest("Order items required");

        //     var booking = await _context.Bookings.FindAsync(request.BookingId);

        //     if (booking == null)
        //         return NotFound("Booking not found");

        //     if (booking.Status != BookingStatus.Seated)
        //         return BadRequest("Booking must be seated");

        //     // 🔥 CHECK EXISTING ACTIVE ORDER
        //     var existingOrder = await _context.Orders
        //         .Include(o => o.OrderItems)
        //         .FirstOrDefaultAsync(o =>
        //             o.BookingId == request.BookingId &&
        //             o.Status != OrderStatus.Billed &&
        //             o.Status != OrderStatus.Completed);

        //     if (existingOrder != null)
        //     {
        //         foreach (var i in request.Items)
        //         {
        //             var menu = await _context.MenuItems.FindAsync(i.MenuItemId);

        //             if (menu == null)
        //                 return BadRequest("Invalid menu item");

        //             var item = new OrderItem
        //             {
        //                 MenuItemId = i.MenuItemId,
        //                 Quantity = i.Quantity,
        //                 UnitPrice = menu.Price,
        //                 TotalPrice = menu.Price * i.Quantity,
        //                 KitchenStatus = KitchenStatus.Pending
        //             };

        //             existingOrder.OrderItems.Add(item);
        //             existingOrder.TotalAmount += item.TotalPrice;
        //         }

        //         await _context.SaveChangesAsync();
        //         return Ok(existingOrder);
        //     }

        //     // =========================
        //     // CREATE NEW ORDER
        //     // =========================

        //     var last = await _context.Orders
        //         .OrderByDescending(o => o.OrderId)
        //         .Select(o => o.OrderNumber)
        //         .FirstOrDefaultAsync();

        //     int next = 1;
        //     if (!string.IsNullOrEmpty(last))
        //         next = int.Parse(last.Replace("ORD-", "")) + 1;

        //     var order = new Order
        //     {
        //         OrderNumber = $"ORD-{next:D4}",
        //         BookingId = booking.BookingId,
        //         TableId = booking.TableId,
        //         OrderDate = DateTime.Now,
        //         Status = OrderStatus.Pending,
        //         OrderItems = new List<OrderItem>()
        //     };

        //     decimal total = 0;

        //     foreach (var i in request.Items)
        //     {
        //         var menu = await _context.MenuItems.FindAsync(i.MenuItemId);

        //         if (menu == null)
        //             return BadRequest("Invalid menu item");

        //         var item = new OrderItem
        //         {
        //             MenuItemId = i.MenuItemId,
        //             Quantity = i.Quantity,
        //             UnitPrice = menu.Price,
        //             TotalPrice = menu.Price * i.Quantity,
        //             KitchenStatus = KitchenStatus.Pending
        //         };

        //         total += item.TotalPrice;
        //         order.OrderItems.Add(item);
        //     }

        //     order.TotalAmount = total;

        //     _context.Orders.Add(order);
        //     await _context.SaveChangesAsync();

        //     return Ok(order);
        // }

        // 
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                // =========================
                // VALIDATION
                // =========================
                if (request == null || request.Items == null)
                    return BadRequest("Order items required");

                var booking = await _context.Bookings
                    .FirstOrDefaultAsync(b => b.BookingId == request.BookingId);

                if (booking == null)
                    return NotFound("Booking not found");

                if (booking.Status != BookingStatus.Seated)
                    return BadRequest("Booking must be seated before ordering");

                var tableExists = await _context.DiningTables
                    .AnyAsync(t => t.TableId == booking.TableId);

                if (!tableExists)
                    return BadRequest("Invalid table assigned to booking");
                // =========================
                // CHECK EXISTING ORDER
                // =========================
                var existingOrder = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o =>
                        o.BookingId == request.BookingId &&
                        o.Status != OrderStatus.Billed &&
                        o.Status != OrderStatus.Completed);

                if (existingOrder != null)
                {
                    if (existingOrder.OrderItems == null)
                        existingOrder.OrderItems = new List<OrderItem>();

                    foreach (var i in request.Items)
                    {
                        var menu = await _context.MenuItems
                            .FirstOrDefaultAsync(m => m.MenuItemId == i.MenuItemId);

                        if (menu == null)
                            return BadRequest($"Invalid menu item: {i.MenuItemId}");

                        var newItem = new OrderItem
                        {
                            MenuItemId = menu.MenuItemId,
                            Quantity = i.Quantity,
                            UnitPrice = menu.Price,
                            TotalPrice = menu.Price * i.Quantity,
                            KitchenStatus = KitchenStatus.Pending
                        };

                        existingOrder.OrderItems.Add(newItem);
                        existingOrder.TotalAmount += newItem.TotalPrice;
                    }

                    await _context.SaveChangesAsync();

                    return Ok(existingOrder);
                }

                // =========================
                // CREATE NEW ORDER
                // =========================
                var order = new Order
                {
                    BookingId = booking.BookingId,
                    TableId = booking.TableId,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    OrderItems = new List<OrderItem>()
                };

                decimal total = 0;

                foreach (var i in request.Items)
                {
                    var menu = await _context.MenuItems
                        .FirstOrDefaultAsync(m => m.MenuItemId == i.MenuItemId);

                    if (menu == null)
                        return BadRequest($"Invalid menu item: {i.MenuItemId}");

                    var item = new OrderItem
                    {
                        MenuItemId = menu.MenuItemId,
                        Quantity = i.Quantity,
                        UnitPrice = menu.Price,
                        TotalPrice = menu.Price * i.Quantity,
                        KitchenStatus = KitchenStatus.Pending
                    };

                    order.OrderItems.Add(item);
                    total += item.TotalPrice;
                }

                order.TotalAmount = total;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return Ok(order);
            }
            catch (Exception ex)
            {
                // 🔥 SAFE ERROR RESPONSE (IMPORTANT)
                return StatusCode(500, new
                {
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        // =========================
        // GET ORDERS
        // =========================
        // [HttpGet]
        // public async Task<IActionResult> Get()
        // {
        //     return Ok(await _context.Orders.ToListAsync());
        // }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _context.Orders
                .Where(o => o.Status != OrderStatus.Billed &&
                            o.Status != OrderStatus.Completed)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.MenuItem)
                .ToListAsync();

            return Ok(orders);
        }

        // =========================
        // GET ORDER DETAILS
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.MenuItem)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // =========================
        // GET ACTIVE ORDER BY TABLE
        // =========================
        [HttpGet("table/{tableId}")]
        public async Task<IActionResult> GetByTable(int tableId)
        {
            var orders = await _context.Orders
                .Where(o =>
                    o.TableId == tableId &&
                    o.Status != OrderStatus.Billed &&
                    o.Status != OrderStatus.Completed)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.MenuItem)
                .ToListAsync();

            if (!orders.Any())
                return NotFound();

            var total = orders.Sum(o => o.TotalAmount);

            var items = orders
                .SelectMany(o => o.OrderItems)
                .Select(i => new
                {
                    i.MenuItem,
                    i.Quantity,
                    i.TotalPrice
                });

            return Ok(new
            {
                TableId = tableId,
                Orders = orders,
                OrderItems = items,
                TotalAmount = total
            });
        }

        // =========================
        // UPDATE STATUS
        // =========================
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.Status = status;

            if (status == OrderStatus.Billed)
            {
                order.PaidAmount = order.TotalAmount;
                order.PaidDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return Ok(order);
        }
        [HttpPut("table/{tableId}/bill")]
        public async Task<IActionResult> BillTableOrders(int tableId)
        {
            var orders = await _context.Orders
                .Where(o =>
                    o.TableId == tableId &&
                    o.Status != OrderStatus.Billed &&
                    o.Status != OrderStatus.Completed)
                .ToListAsync();

            if (!orders.Any())
                return BadRequest("No active orders to bill");

            foreach (var order in orders)
            {
                order.Status = OrderStatus.Completed;
                order.PaidAmount = order.TotalAmount;
                order.PaidDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "✅ All orders billed successfully"
            });
        }
    }
}