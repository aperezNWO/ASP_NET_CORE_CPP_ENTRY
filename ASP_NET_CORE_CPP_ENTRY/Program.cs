using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Pruebas.Cliente.Models;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Swagger generator
//Builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP_NET_CORE_CPP_ENTRY", Version = "v1" });
});

// Register Swagger generator
builder.Services.AddEndpointsApiExplorer();


//
builder.Services.AddDbContext<SigaFfmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

//
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});


//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use exception handler early
    app.UseExceptionHandler("/Home/Error");
}

// === 🔑 CORRECT MIDDLEWARE ORDER BELOW ===

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP_NET_CORE_CPP_ENTRY");
});

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