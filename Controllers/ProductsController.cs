using Microsoft.AspNetCore.Mvc;
using GestionePuntiVendita.Data;
using GestionePuntiVendita.Models;

namespace GestionePuntiVendita.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;

        // Seed iniziale: aggiunge dati solo se il DB è vuoto
        if (!_context.Products.Any())
        {
            _context.Products.AddRange(
                new Product { Id = 1, Sku = "BEV-001", Name = "Acqua Naturale 0.5L", Category = "Bevande", UnitPrice = 1.00m },
                new Product { Id = 2, Sku = "BEV-002", Name = "Caffè Espresso", Category = "Bevande", UnitPrice = 1.20m },
                new Product { Id = 3, Sku = "ALM-001", Name = "Panino Prosciutto", Category = "Alimentari", UnitPrice = 4.50m },
                new Product { Id = 4, Sku = "ELT-001", Name = "Dolce", Category = "Alimentari", UnitPrice = 8.90m }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }
}
