using Microsoft.AspNetCore.Mvc;
using GestionePuntiVendita.Data;
using GestionePuntiVendita.Models;

namespace GestionePuntiVendita.Controllers;

/// <summary>
/// Gestisce i prodotti del punto vendita.
/// </summary>
/// <remarks>
/// Endpoint disponibili:
/// - <b>GET /api/products</b> → Restituisce tutti i prodotti
/// 
/// I prodotti includono:
/// - Nome
/// - Prezzo unitario
/// - Categoria (es. Bevande, Alimentari)
/// - Codice univoco (SKU)
/// </remarks>
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
                new Product { Id = 4, Sku = "ALM-002", Name = "Dolce", Category = "Alimentari", UnitPrice = 8.90m }
            );
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Restituisce tutti i prodotti disponibili.
    /// </summary>
    /// <remarks>
    /// Esempio di risposta:
    ///
    ///     GET /api/products
    ///     [
    ///        {
    ///          "id": 1,
    ///          "sku": "BEV-001",
    ///          "name": "Acqua Naturale 0.5L",
    ///          "category": "Bevande",
    ///          "unitPrice": 1.00
    ///        }
    ///     ]
    ///
    /// </remarks>
    /// <returns>Una lista di prodotti presenti nel sistema.</returns>
    /// <response code="200">Restituisce la lista dei prodotti</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetProducts()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }
}
