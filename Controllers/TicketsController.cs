using Microsoft.AspNetCore.Mvc;
using GestionePuntiVendita.Data;
using GestionePuntiVendita.Models;

namespace GestionePuntiVendita.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /api/tickets
    [HttpGet]
    public IActionResult GetAll()
    {
        var tickets = _context.Tickets
            .Select(t => new
            {
                t.Id,
                t.Date,
                Total = t.Total,
                Items = t.Items.Select(i => new
                {
                    i.ProductName,
                    i.Quantity,
                    i.UnitPrice,
                    Subtotal = i.Subtotal
                })
            })
            .ToList();

        return Ok(tickets);
    }

    // POST: /api/tickets
    [HttpPost]
    public IActionResult Create([FromBody] List<TicketItem> items)
    {
        if (items == null || !items.Any())
            return BadRequest(new { error = "Il ticket deve contenere almeno un prodotto." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Controlli manuali extra
        foreach (var item in items)
        {
            if (item.Quantity <= 0)
                return BadRequest(new { error = $"Quantità non valida per {item.ProductName}" });

            if (item.UnitPrice <= 0)
                return BadRequest(new { error = $"Prezzo non valido per {item.ProductName}" });
        }

        var ticket = new Ticket { Items = items };

        try
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return Ok(ticket);
        }
        catch (Exception ex)
        {
            return Problem("Errore durante il salvataggio del ticket: " + ex.Message);
        }
    }

}
