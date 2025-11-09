namespace GestionePuntiVendita.Models;

public class TicketItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
    public decimal Subtotal => UnitPrice * Quantity;

    public string ProductName { get; set; } = default!;
}
