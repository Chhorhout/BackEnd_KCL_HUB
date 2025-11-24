using IDP.Api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using IDP.Api.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(); // Better JSON handling for GUIDs
builder.Services.AddDbContext<ApplicationDbContext>(action =>
{
    action.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5000",
        builder =>
        {
            builder.WithOrigins("http://localhost:5000", "http://192.168.189.1:5000","http://192.168.2.214:5000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithExposedHeaders("X-Total-Count", "X-Total-Pages", "X-Current-Page", "X-Page-Size");
        });
});

// Configure to listen on all network interfaces (0.0.0.0)
builder.WebHost.UseUrls("http://0.0.0.0:5165");

var app = builder.Build();

// Seed database with initial roles
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        await context.Database.EnsureCreatedAsync();
        await DbSeeder.SeedRolesAsync(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Middleware order is critical:
// 1. Routing must be first
app.UseRouting();

// 2. CORS must be after routing, before endpoints
app.UseCors("AllowLocalhost5000");

// 3. MapControllers must be after CORS
app.MapControllers();

// Health check endpoint at /api for connectivity testing
app.MapGet("/api", () => new { status = "ok", message = "IDP API is running", timestamp = DateTime.UtcNow })
    .WithTags("Health");

// Optional: Static files (if needed)
app.UseStaticFiles();

app.Run();
