using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "TableNumber",
                value: "Royal-01");

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 2,
                columns: new[] { "Capacity", "TableNumber" },
                values: new object[] { 2, "Royal-02" });

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 3,
                columns: new[] { "Capacity", "TableNumber" },
                values: new object[] { 4, "Garden-01" });

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "TableNumber",
                value: "Garden-02");

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 5,
                columns: new[] { "Capacity", "TableNumber" },
                values: new object[] { 6, "Family-01" });

            migrationBuilder.InsertData(
                table: "DiningTables",
                columns: new[] { "TableId", "Capacity", "Status", "TableNumber" },
                values: new object[,]
                {
                    { 6, 6, 0, "Family-02" },
                    { 7, 8, 0, "VIP-01" },
                    { 8, 8, 0, "VIP-02" },
                    { 9, 4, 0, "Terrace-01" },
                    { 10, 2, 0, "Terrace-02" }
                });

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "MenuCategoryId", "Code", "Name" },
                values: new object[,]
                {
                    { 4, "DS", "Desserts" },
                    { 5, "FF", "Fast Food" },
                    { 6, "SI", "South Indian" }
                });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1,
                column: "Name",
                value: "Tomato Soup");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Grilled paneer", "Paneer Tikka", 180m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 3,
                columns: new[] { "Description", "MenuCategoryId", "Name", "Price" },
                values: new object[] { "Spicy wings", 1, "Chicken Wings", 220m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 4,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Spicy curry", "Chicken Curry", 250m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Special biryani", "Mutton Biryani", 320m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 6,
                columns: new[] { "Description", "MenuCategoryId", "Name", "Price" },
                values: new object[] { "Veg rice", 2, "Veg Fried Rice", 180m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 7,
                columns: new[] { "Description", "MenuCategoryId", "Name", "Price" },
                values: new object[] { "Soft naan", 2, "Butter Naan", 40m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Fresh juice", "Mango Juice", 90m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuItemId", "Description", "IsAvailable", "MenuCategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 9, "Chilled coffee", true, 3, "Cold Coffee", 120m },
                    { 10, "Refreshing drink", true, 3, "Lemon Soda", 70m },
                    { 11, "Vanilla scoop", true, 4, "Ice Cream", 100m },
                    { 12, "Sweet dessert", true, 4, "Gulab Jamun", 80m },
                    { 13, "Crispy burger", true, 5, "Veg Burger", 150m },
                    { 14, "Chicken burger", true, 5, "Chicken Burger", 180m },
                    { 15, "Crispy fries", true, 5, "French Fries", 120m },
                    { 16, "Crispy dosa", true, 6, "Masala Dosa", 90m },
                    { 17, "Soft idli", true, 6, "Idli", 50m },
                    { 18, "Crispy vada", true, 6, "Vada", 60m },
                    { 19, "South breakfast", true, 6, "Pongal", 70m },
                    { 20, "Traditional meal", true, 6, "Sambar Rice", 110m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "MenuCategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "MenuCategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "MenuCategoryId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "TableNumber",
                value: "T1");

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 2,
                columns: new[] { "Capacity", "TableNumber" },
                values: new object[] { 4, "T2" });

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 3,
                columns: new[] { "Capacity", "TableNumber" },
                values: new object[] { 6, "T3" });

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "TableNumber",
                value: "T4");

            migrationBuilder.UpdateData(
                table: "DiningTables",
                keyColumn: "TableId",
                keyValue: 5,
                columns: new[] { "Capacity", "TableNumber" },
                values: new object[] { 8, "T5" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1,
                column: "Name",
                value: "Soup");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Fresh salad", "Salad", 100m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 3,
                columns: new[] { "Description", "MenuCategoryId", "Name", "Price" },
                values: new object[] { "Spicy curry", 2, "Chicken Curry", 250m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 4,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Chicken biryani", "Biryani", 300m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 5,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Veg fried rice", "Fried Rice", 180m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 6,
                columns: new[] { "Description", "MenuCategoryId", "Name", "Price" },
                values: new object[] { "Fresh juice", 3, "Juice", 80m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 7,
                columns: new[] { "Description", "MenuCategoryId", "Name", "Price" },
                values: new object[] { "Hot coffee", 3, "Coffee", 60m });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Masala tea", "Tea", 40m });
        }
    }
}
