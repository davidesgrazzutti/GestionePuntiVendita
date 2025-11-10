using GestionePuntiVendita.Data;
using GestionePuntiVendita.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// --- Servizi ---
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=gestione.db"));


// --- SERVIZI ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gestione Punti Vendita API",
        Version = "v1",
        Description = "API REST per la gestione di prodotti e scontrini in un punto vendita. " +
                      "Include endpoint per la creazione di scontrini, gestione prodotti e storico vendite.",
        Contact = new OpenApiContact
        {
            Name = "Davide Sgrazzutti",
            Url = new Uri("https://github.com/davidesgrazzutti/GestionePuntiVendita")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


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
            new Product { Sku = "ALM-002", Name = "Dolce", Category = "Alimentari", UnitPrice = 8.90m }
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

app.UseCors();
app.Run();
