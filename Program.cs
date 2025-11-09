using GestionePuntiVendita.Data;
using GestionePuntiVendita.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Servizi ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=gestione.db"));


// --- SERVIZI ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Crea il DB se non esiste
    db.Database.EnsureCreated();

    // Seed prodotti solo la prima volta
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Sku = "BEV-001", Name = "Acqua Naturale 0.5L", Category = "Bevande", UnitPrice = 1.00m },
            new Product { Sku = "BEV-002", Name = "Caffè Espresso", Category = "Bevande", UnitPrice = 1.20m },
            new Product { Sku = "ALM-001", Name = "Panino Prosciutto", Category = "Alimentari", UnitPrice = 4.50m },
            new Product { Sku = "ELT-001", Name = "Cavo USB-C", Category = "Elettronica", UnitPrice = 8.90m }
        );
        db.SaveChanges();
    }
}


// --- PIPELINE ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // disattivato per ora
app.UseAuthorization();

app.MapControllers();

// Endpoint di test
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
