namespace GestionePuntiVendita.Models
{
    /// <summary>
    /// Rappresenta un prodotto disponibile nel punto vendita.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        /// <summary>
        /// Codice univoco del prodotto (SKU).
        /// </summary>
        public string Sku { get; set; } = default!;

        /// <summary>
        /// Nome descrittivo del prodotto.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Categoria di appartenenza (es. Bevande, Alimentari, Dolci, ecc.).
        /// </summary>
        public string Category { get; set; } = default!;

        /// <summary>
        /// Prezzo unitario del prodotto.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
