// using Microsoft.EntityFrameworkCore;
// using RestaurantAPI.Data;
// using System.Text.Json.Serialization;

// var builder = WebApplication.CreateBuilder(args);

// // Controllers
// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.ReferenceHandler =
//             ReferenceHandler.IgnoreCycles;
//     });

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // CORS
// // builder.Services.AddCors(options =>
// // {
// //     options.AddPolicy("AllowReactApp", policy =>
// //     {
// //         policy.WithOrigins(
// //                 "http://localhost:3000",
// //                 "https://restaurant-booking-j4kf.onrender.com"
// //             )
// //             .AllowAnyHeader()
// //             .AllowAnyMethod();
// //     });
// // });
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowReactApp", policy =>
//     {
//         policy
//             .AllowAnyHeader()
//             .AllowAnyMethod()
//             .SetIsOriginAllowed(origin =>
//                 origin == "https://restaurant-booking-j4kf.onrender.com"
//             );
//     });
// });

// // DB Context
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// if (string.IsNullOrWhiteSpace(connectionString))
// {
//     throw new Exception("Database connection string is missing in appsettings.json");
// }

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(connectionString)
// );

// var app = builder.Build();

// // Swagger
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseCors("AllowReactApp");

// app.UseAuthorization();

// app.MapControllers();

// // AUTO MIGRATION
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     db.Database.Migrate();
// }

// app.Run();
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// =======================
// CORS (MUST BE SIMPLE IN PRODUCTION)
// =======================
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend", policy =>
//     {
//         policy
//             .WithOrigins(
//                 "https://restaurant-booking-j4kf.onrender.com"
//             )
//             .AllowAnyOrigin()
//             .AllowAnyHeader()
//             .AllowAnyMethod()
//             .AllowCredentials();
//     });
// });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "https://restaurant-booking-j4kf.onrender.com"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// =======================
// DB
// =======================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);


var app = builder.Build();


// =======================
// IMPORTANT ORDER
// =======================
app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();


// =======================
// AUTO MIGRATION
// =======================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();