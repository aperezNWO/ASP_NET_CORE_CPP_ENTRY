using ASP_NET_CORE_CPP_ENTRY.Services;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using Pruebas.Cliente.Models;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SigaFfmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});

builder.Services.AddScoped<TetrisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use exception handler early
    app.UseExceptionHandler("/Home/Error");
}

// === 🔑 CORRECT MIDDLEWARE ORDER BELOW ===

app.UseCors(MyAllowSpecificOrigins); // ← Must come BEFORE UseRouting

app.UseStaticFiles(); // Serve static files (CSS, JS, images)

app.UseRouting(); // Required for endpoint routing

app.UseAuthorization(); // Check auth after routing (but before endpoints)

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers(); // For attribute-routed API controllers

// Optional: call Run() last
app.Run();