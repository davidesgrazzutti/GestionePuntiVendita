using System.ComponentModel.DataAnnotations;

namespace GestionePuntiVendita.Models
{
    /// <summary>
    /// Rappresenta una singola voce dello scontrino (prodotto acquistato).
    /// </summary>
    public class TicketItem
    {
        public int Id { get; set; }

        [Required]
        public string ProductSku { get; set; } = default!;

        [Required]
        public string ProductName { get; set; } = default!;

        [Range(1, int.MaxValue, ErrorMessage = "La quantità deve essere almeno 1")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Subtotale per il prodotto (quantità × prezzo unitario).
        /// </summary>
        public decimal Subtotal => UnitPrice * Quantity;
    }
}
