using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ CORS (ALLOW REACT)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ✅ DATABASE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// SWAGGER
app.UseSwagger();
app.UseSwaggerUI();

// REMOVE THIS (NOT REQUIRED IN MODERN MINIMAL PIPELINE)
// app.UseRouting();

app.UseCors("AllowReactApp"); // MUST be before controllers

app.UseAuthorization();

app.MapControllers();

app.Run();