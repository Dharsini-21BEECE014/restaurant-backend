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
            return Ok(await _context.MenuItems
                .Include(m => m.MenuCategory)
                .Where(m => m.IsAvailable)
                .ToListAsync());
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