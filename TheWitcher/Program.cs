
using TheWitcher.Data.Data;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders(); // Usu� inne dostawce log�w
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
    app.UseSwaggerUI(); // Interfejs Swagger w przegl�darce
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // W��cz HSTS na produkcji
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
