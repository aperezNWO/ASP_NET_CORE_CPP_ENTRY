using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;
using Pruebas.Cliente.Repositorio;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SigaFfmContext>(options => options.UseSqlServer(builder.Configuration.
    GetConnectionString("defaultConnection")));

builder.Services.AddScoped<IUsuario, Repositorio_Usuario>();
builder.Services.AddScoped<IProducto, Repositorio_Producto>();
builder.Services.AddScoped<IPersonal, Repositorio_Personal>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors(MyAllowSpecificOrigins);

app.Run();
