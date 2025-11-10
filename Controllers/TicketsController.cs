using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionePuntiVendita.Data;
using GestionePuntiVendita.Models;
using System.Linq;

namespace GestionePuntiVendita.Controllers;

/// <summary>
/// Gestisce la creazione e la consultazione degli scontrini (ticket).
/// </summary>
/// <remarks>
/// Gli endpoint permettono di:
/// - Visualizzare lo storico degli scontrini
/// - Creare un nuovo scontrino con prodotti e quantità
///
/// Ogni scontrino contiene:
/// - Data di emissione
/// - Totale calcolato automaticamente
/// - Lista dettagliata dei prodotti
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Restituisce la lista di tutti gli scontrini emessi.
    /// </summary>
    /// <remarks>
    /// Gli scontrini vengono restituiti in ordine decrescente (dal più recente al più vecchio).
    ///
    /// Esempio di risposta:
    ///
    ///     GET /api/tickets
    ///     [
    ///        {
    ///          "id": 5,
    ///          "date": "2025-11-09T22:30:00",
    ///          "total": 12.50,
    ///          "items": [
    ///             { "productName": "Caffè Espresso", "quantity": 2, "unitPrice": 1.20, "subtotal": 2.40 },
    ///             { "productName": "Panino Prosciutto", "quantity": 2, "unitPrice": 4.50, "subtotal": 9.00 }
    ///          ]
    ///        }
    ///     ]
    /// </remarks>
    /// <response code="200">Restituisce la lista completa degli scontrini</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _context.Tickets
            .OrderByDescending(t => t.Date)
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
            .ToListAsync();

        return Ok(tickets);
    }

    /// <summary>
    /// Crea un nuovo scontrino a partire da una lista di prodotti.
    /// </summary>
    /// <param name="items">Lista dei prodotti acquistati, con quantità e prezzo unitario.</param>
    /// <remarks>
    /// Ogni oggetto deve includere:
    /// 
    ///     {
    ///       "productId": 1,
    ///       "productName": "Caffè Espresso",
    ///       "quantity": 2,
    ///       "unitPrice": 1.20
    ///     }
    ///
    /// Esempio di richiesta:
    ///
    ///     POST /api/tickets
    ///     [
    ///        { "productId": 1, "productName": "Caffè Espresso", "quantity": 2, "unitPrice": 1.20 },
    ///        { "productId": 3, "productName": "Panino Prosciutto", "quantity": 1, "unitPrice": 4.50 }
    ///     ]
    ///
    /// Esempio di risposta:
    ///
    ///     {
    ///       "id": 6,
    ///       "date": "2025-11-09T22:55:00",
    ///       "total": 6.90,
    ///       "items": [...]
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Scontrino creato con successo</response>
    /// <response code="400">Richiesta non valida (es. prodotti mancanti o quantità non valida)</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] List<TicketItem> items)
    {
        if (items == null || !items.Any())
            return BadRequest(new { error = "Il ticket deve contenere almeno un prodotto." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Validazioni manuali
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
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }
        catch (Exception ex)
        {
            return Problem("Errore durante il salvataggio del ticket: " + ex.Message);
        }
    }
}
