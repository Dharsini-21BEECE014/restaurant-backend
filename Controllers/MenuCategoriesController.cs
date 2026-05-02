using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/menu-categories")]
    public class MenuCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.MenuCategories.ToListAsync();
            return Ok(categories);
        }
    }
}