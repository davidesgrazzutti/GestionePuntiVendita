using NUnit.Framework;
using GestionePuntiVendita.Models;

namespace GestionePuntiVendita.Tests
{
    [TestFixture]
    public class TicketUnitTests
    {
        [Test]
        public void CalcolaTotale_TicketConDueProdotti_RestituisceSommaCorretta()
        {
            var ticket = new Ticket
            {
                Items = new List<TicketItem>
                {
                    new TicketItem { ProductName = "Caffè", Quantity = 2, UnitPrice = 1.20m },
                    new TicketItem { ProductName = "Panino", Quantity = 1, UnitPrice = 4.50m }
                }
            };

            var totale = ticket.Total;

            Assert.That(totale, Is.EqualTo(6.90m));
        }

        [Test]
        public void CalcolaTotale_SenzaProdotti_RestituisceZero()
        {
            var ticket = new Ticket { Items = new List<TicketItem>() };

            var totale = ticket.Total;

            Assert.That(totale, Is.EqualTo(0m));
        }

        [Test]
        public void CalcolaTotale_QuantitaZero_RestituisceZero()
        {
            var ticket = new Ticket
            {
                Items = new List<TicketItem>
                {
                    new TicketItem { ProductName = "Errore", Quantity = 0, UnitPrice = 10m }
                }
            };

            var totale = ticket.Total;

            Assert.That(totale, Is.EqualTo(0m));
        }

        [Test]
        public void CalcolaTotale_PrezzoNegativo_RestituisceZero()
        {
            var ticket = new Ticket
            {
                Items = new List<TicketItem>
                {
                    new TicketItem { ProductName = "Errore", Quantity = 1, UnitPrice = -5m }
                }
            };

            var totale = ticket.Total;

            Assert.That(totale, Is.EqualTo(0m));
        }
    }
}
