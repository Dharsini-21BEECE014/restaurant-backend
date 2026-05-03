using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/menu-items")]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Console.WriteLine("🔥 Menu API called");

                var items = await _context.MenuItems
                    .Include(m => m.MenuCategory)
                    .Where(m => m.IsAvailable == true)
                    .ToListAsync();

                return Ok(items.Select(m => new
                {
                    m.MenuItemId,
                    m.Name,
                    m.Price,
                    m.Description,
                    Category = m.MenuCategory != null ? m.MenuCategory.Name : "No Category"
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ MENU ERROR: " + ex.Message);
                Console.WriteLine("❌ INNER: " + ex.InnerException?.Message);

                return StatusCode(500, new
                {
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> ByCategory(int id)
        {
            return Ok(await _context.MenuItems
                .Where(m => m.MenuCategoryId == id)
                .ToListAsync());
        }
    }
}