using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<DiningTable> DiningTables { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================= MENU CATEGORIES =================
            modelBuilder.Entity<MenuCategory>().HasData(
                new MenuCategory { MenuCategoryId = 1, Name = "Starters", Code = "ST" },
                new MenuCategory { MenuCategoryId = 2, Name = "Main Course", Code = "MC" },
                new MenuCategory { MenuCategoryId = 3, Name = "Drinks", Code = "DR" },
                new MenuCategory { MenuCategoryId = 4, Name = "Desserts", Code = "DS" },
                new MenuCategory { MenuCategoryId = 5, Name = "Fast Food", Code = "FF" },
                new MenuCategory { MenuCategoryId = 6, Name = "South Indian", Code = "SI" }
            );

            // ================= MENU ITEMS =================
            modelBuilder.Entity<MenuItem>().HasData(
                // Starters
                new MenuItem { MenuItemId = 1, Name = "Tomato Soup", MenuCategoryId = 1, Price = 120, Description = "Hot soup" },
                new MenuItem { MenuItemId = 2, Name = "Paneer Tikka", MenuCategoryId = 1, Price = 180, Description = "Grilled paneer" },
                new MenuItem { MenuItemId = 3, Name = "Chicken Wings", MenuCategoryId = 1, Price = 220, Description = "Spicy wings" },

                // Main Course
                new MenuItem { MenuItemId = 4, Name = "Chicken Curry", MenuCategoryId = 2, Price = 250, Description = "Spicy curry" },
                new MenuItem { MenuItemId = 5, Name = "Mutton Biryani", MenuCategoryId = 2, Price = 320, Description = "Special biryani" },
                new MenuItem { MenuItemId = 6, Name = "Veg Fried Rice", MenuCategoryId = 2, Price = 180, Description = "Veg rice" },
                new MenuItem { MenuItemId = 7, Name = "Butter Naan", MenuCategoryId = 2, Price = 40, Description = "Soft naan" },

                // Drinks
                new MenuItem { MenuItemId = 8, Name = "Mango Juice", MenuCategoryId = 3, Price = 90, Description = "Fresh juice" },
                new MenuItem { MenuItemId = 9, Name = "Cold Coffee", MenuCategoryId = 3, Price = 120, Description = "Chilled coffee" },
                new MenuItem { MenuItemId = 10, Name = "Lemon Soda", MenuCategoryId = 3, Price = 70, Description = "Refreshing drink" },

                // Desserts
                new MenuItem { MenuItemId = 11, Name = "Ice Cream", MenuCategoryId = 4, Price = 100, Description = "Vanilla scoop" },
                new MenuItem { MenuItemId = 12, Name = "Gulab Jamun", MenuCategoryId = 4, Price = 80, Description = "Sweet dessert" },

                // Fast Food
                new MenuItem { MenuItemId = 13, Name = "Veg Burger", MenuCategoryId = 5, Price = 150, Description = "Crispy burger" },
                new MenuItem { MenuItemId = 14, Name = "Chicken Burger", MenuCategoryId = 5, Price = 180, Description = "Chicken burger" },
                new MenuItem { MenuItemId = 15, Name = "French Fries", MenuCategoryId = 5, Price = 120, Description = "Crispy fries" },

                // South Indian
                new MenuItem { MenuItemId = 16, Name = "Masala Dosa", MenuCategoryId = 6, Price = 90, Description = "Crispy dosa" },
                new MenuItem { MenuItemId = 17, Name = "Idli", MenuCategoryId = 6, Price = 50, Description = "Soft idli" },
                new MenuItem { MenuItemId = 18, Name = "Vada", MenuCategoryId = 6, Price = 60, Description = "Crispy vada" },
                new MenuItem { MenuItemId = 19, Name = "Pongal", MenuCategoryId = 6, Price = 70, Description = "South breakfast" },
                new MenuItem { MenuItemId = 20, Name = "Sambar Rice", MenuCategoryId = 6, Price = 110, Description = "Traditional meal" }
            );

            // ================= TABLES (UNIQUE NAMES) =================
            modelBuilder.Entity<DiningTable>().HasData(
                new DiningTable { TableId = 1, TableNumber = "Royal-01", Capacity = 2, Status = TableStatus.Available },
                new DiningTable { TableId = 2, TableNumber = "Royal-02", Capacity = 2, Status = TableStatus.Available },
                new DiningTable { TableId = 3, TableNumber = "Garden-01", Capacity = 4, Status = TableStatus.Available },
                new DiningTable { TableId = 4, TableNumber = "Garden-02", Capacity = 4, Status = TableStatus.Available },
                new DiningTable { TableId = 5, TableNumber = "Family-01", Capacity = 6, Status = TableStatus.Available },
                new DiningTable { TableId = 6, TableNumber = "Family-02", Capacity = 6, Status = TableStatus.Available },
                new DiningTable { TableId = 7, TableNumber = "VIP-01", Capacity = 8, Status = TableStatus.Available },
                new DiningTable { TableId = 8, TableNumber = "VIP-02", Capacity = 8, Status = TableStatus.Available },
                new DiningTable { TableId = 9, TableNumber = "Terrace-01", Capacity = 4, Status = TableStatus.Available },
                new DiningTable { TableId = 10, TableNumber = "Terrace-02", Capacity = 2, Status = TableStatus.Available }
            );
        }
    }
}