using AMS.Api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using AMS.Api.Mapping;
using AMS.Api.Data.Configs;
using AMS.Api.Models;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbcontext>(action => {
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
var app = builder.Build();

// Middleware order is critical:
// 1. Routing must be first
app.UseRouting();

// 2. CORS must be after routing, before endpoints
app.UseCors("AllowLocalhost5000");

// 3. MapControllers must be after CORS
app.MapControllers();

// Optional: Static files (if needed)
app.UseStaticFiles();

app.Run();
