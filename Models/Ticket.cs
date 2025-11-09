namespace GestionePuntiVendita.Models;

public class Ticket
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public List<TicketItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Subtotal);
}
