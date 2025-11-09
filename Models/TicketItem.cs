using System.ComponentModel.DataAnnotations;

namespace GestionePuntiVendita.Models;

public class TicketItem
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere almeno 1")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero")]
    public decimal UnitPrice { get; set; }

    public decimal Subtotal => UnitPrice * Quantity;

    [Required]
    public string ProductName { get; set; } = default!;
}
