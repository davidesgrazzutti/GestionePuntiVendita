using System.ComponentModel.DataAnnotations;

namespace GestionePuntiVendita.Models
{
    /// <summary>
    /// Rappresenta un prodotto disponibile nel punto vendita.
    /// Lo SKU è utilizzato come chiave primaria.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Codice univoco del prodotto (SKU). Funge da CHIAVE PRIMARIA.
        /// </summary>
        [Key] // Dichiara Sku come chiave primaria
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