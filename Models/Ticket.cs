using System.ComponentModel.DataAnnotations;

namespace GestionePuntiVendita.Models
{
    /// <summary>
    /// Rappresenta uno scontrino del punto vendita.
    /// Contiene la data di emissione, i prodotti acquistati e il totale calcolato automaticamente.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Identificativo univoco dello scontrino.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data e ora di emissione dello scontrino.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Elenco dei prodotti acquistati (voci dello scontrino).
        /// </summary>
        public List<TicketItem> Items { get; set; } = new();

        /// <summary>
        /// Totale complessivo calcolato automaticamente.
        /// Se un item ha quantità o prezzo negativo, viene ignorato.
        /// </summary>
        public decimal Total =>
            Items?.Sum(i =>
                (i.Quantity > 0 && i.UnitPrice > 0)
                    ? i.Subtotal
                    : 0m
            ) ?? 0m;
    }
}
