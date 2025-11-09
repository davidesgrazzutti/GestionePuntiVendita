namespace GestionePuntiVendita.Models;

public class Product
{
    public int Id { get; set; }
    public string Sku { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Category { get; set; } = default!;
    public decimal UnitPrice { get; set; }
}
