
using TheWitcher.Data.Data;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // Usuñ inne dostawce logów
builder.Logging.AddConsole(); // Dodaj logowanie do konsoli

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var app = builder.Build();






app.UseMiddleware<RequestLoggingMiddleware>();
// Configure the HTTP request pipeline.





if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Generowanie dokumentacji API
    app.UseSwaggerUI(); // Interfejs Swagger w przegl¹darce
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // W³¹cz HSTS na produkcji
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
