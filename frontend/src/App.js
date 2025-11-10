import React, { useEffect, useState } from "react";
import "./App.css";

const API_BASE = "http://localhost:5000"; // backend .NET

function App() {
  const [products, setProducts] = useState([]);
  const [ticketItems, setTicketItems] = useState([]);
  const [total, setTotal] = useState(0);
  const [ticketsHistory, setTicketsHistory] = useState([]);

  //  Carica prodotti
  useEffect(() => {
    fetch(`${API_BASE}/api/products`)
      .then((res) => res.json())
      .then((data) => setProducts(data))
      .catch(() => alert("Errore nel caricamento dei prodotti"));
  }, []);

  // Carica storico scontrini
  useEffect(() => {
    fetch(`${API_BASE}/api/tickets`)
      .then((res) => res.json())
      .then((data) => setTicketsHistory(data))
      .catch(() => console.error("Errore caricamento storico scontrini"));
  }, []);

  // Aggiungi prodotto allo scontrino
  const addToTicket = (p) => {
    const existing = ticketItems.find((i) => i.productId === p.id);
    let updated;

    if (existing) {
      updated = ticketItems.map((i) =>
        i.productId === p.id ? { ...i, quantity: i.quantity + 1 } : i
      );
    } else {
      updated = [
        ...ticketItems,
        {
          productId: p.id,
          productName: p.name,
          quantity: 1,
          unitPrice: p.unitPrice,
        },
      ];
    }

    setTicketItems(updated);
    calculateTotal(updated);
  };

  //  Diminuisci quantità
  const decreaseQuantity = (id) => {
    let updated = ticketItems
      .map((i) =>
        i.productId === id ? { ...i, quantity: i.quantity - 1 } : i
      )
      .filter((i) => i.quantity > 0);

    setTicketItems(updated);
    calculateTotal(updated);
  };

  // Rimuovi prodotto
  const removeItem = (id) => {
    const updated = ticketItems.filter((i) => i.productId !== id);
    setTicketItems(updated);
    calculateTotal(updated);
  };

  // Calcola totale
  const calculateTotal = (items) => {
    const sum = items.reduce((acc, i) => acc + i.unitPrice * i.quantity, 0);
    setTotal(sum.toFixed(2));
  };

  // Invia ticket al backend
  const submitTicket = () => {
    if (ticketItems.length === 0) {
      alert("Aggiungi almeno un prodotto allo scontrino!");
      return;
    }

    fetch(`${API_BASE}/api/tickets`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(ticketItems),
    })
      .then((res) => res.json())
      .then((data) => {
        alert(`Scontrino creato! Totale: €${data.total.toFixed(2)}`);
        setTicketItems([]);
        setTotal(0);
        refreshHistory();
      })
      .catch(() => alert("Errore durante la creazione dello scontrino"));
  };

  // Ricarica storico
  const refreshHistory = () => {
    fetch(`${API_BASE}/api/tickets`)
      .then((res) => res.json())
      .then((data) => setTicketsHistory(data));
  };

  return (
    <div className="container py-4">
      <h1 className="text-center mb-4">Gestione Punto Vendita</h1>

      {/*  Prodotti */}
      <div className="d-flex flex-wrap justify-content-center gap-3">
        {products.map((p) => (
          <div
            key={p.id}
            className="card shadow-sm"
            style={{
              width: "220px",
              borderRadius: "10px",
              border: "none",
            }}
          >
            <div className="card-body text-center">
              <h5 className="card-title">{p.name}</h5>
              <p className="text-muted mb-1">{p.category}</p>
              <h6 className="fw-bold text-success">€ {p.unitPrice.toFixed(2)}</h6>
              <button
                className="btn btn-primary mt-2 fw-semibold"
                onClick={() => addToTicket(p)}
              >
                ➕ Aggiungi
              </button>
            </div>
          </div>
        ))}
      </div>

      {/*  Scontrino */}
      <div className="mt-5">
        <h2>Scontrino</h2>
        {ticketItems.length === 0 ? (
          <p className="text-muted">Nessun prodotto aggiunto.</p>
        ) : (
          <div className="table-responsive">
            <table className="table table-bordered mt-3 align-middle">
              <thead className="table-light">
                <tr>
                  <th>Prodotto</th>
                  <th className="text-center">Quantità</th>
                  <th>Prezzo Unitario</th>
                  <th>Subtotale</th>
                  <th className="text-center">Azioni</th>
                </tr>
              </thead>
              <tbody>
                {ticketItems.map((i) => (
                  <tr key={i.productId}>
                    <td>{i.productName}</td>
                    <td className="text-center align-middle">
                      <div className="d-flex justify-content-center align-items-center gap-2">
                        <button
                          className="btn btn-sm btn-outline-secondary"
                          onClick={() => decreaseQuantity(i.productId)}
                        >
                          −
                        </button>
                        <span className="fw-bold">{i.quantity}</span>
                        <button
                          className="btn btn-sm btn-outline-secondary"
                          onClick={() =>
                            addToTicket({
                              id: i.productId,
                              name: i.productName,
                              unitPrice: i.unitPrice,
                            })
                          }
                        >
                          +
                        </button>
                      </div>
                    </td>
                    <td>€ {i.unitPrice.toFixed(2)}</td>
                    <td>€ {(i.quantity * i.unitPrice).toFixed(2)}</td>
                    <td className="text-center">
                      <button
                        className="btn btn-sm btn-danger"
                        onClick={() => removeItem(i.productId)}
                      >
                        🗑️ Rimuovi
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr>
                  <th colSpan="4" className="text-end">
                    Totale
                  </th>
                  <th>€ {total}</th>
                </tr>
              </tfoot>
            </table>
          </div>
        )}

        <button
          className="btn btn-success mt-2"
          onClick={submitTicket}
          disabled={ticketItems.length === 0}
        >
          Conferma Scontrino
        </button>
      </div>

      {/*  Storico (sempre visibile) */}
      <div className="mt-5">
        <h2 className="mb-3">Storico Scontrini</h2>

        {ticketsHistory.length === 0 ? (
          <p className="text-muted">Nessuno scontrino emesso.</p>
        ) : (
          ticketsHistory.map((t) => (
            <div key={t.id} className="card mb-3 shadow-sm">
              <div className="card-body">
                <h5 className="card-title">
                  Scontrino #{t.id} —{" "}
                  {t.date
                    ? new Date(t.date).toLocaleString()
                    : "Data non disponibile"}
                </h5>

                <table className="table table-sm w-100 text-center align-middle mt-2">
                  <thead className="table-light">
                    <tr>
                      <th className="text-start">Prodotto</th>
                      <th className="text-center">Q.tà</th>
                      <th className="text-end">Prezzo</th>
                      <th className="text-end">Subtotale</th>
                    </tr>
                  </thead>
                  <tbody>
                    {t.items.map((i, idx) => (
                      <tr key={idx}>
                        <td className="text-start">{i.productName}</td>
                        <td className="text-center">{i.quantity}</td>
                        <td className="text-end">€ {i.unitPrice.toFixed(2)}</td>
                        <td className="text-end">
                          € {(i.quantity * i.unitPrice).toFixed(2)}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>

                <div className="text-end fw-bold me-2">
                  Totale: € {t.total.toFixed(2)}
                </div>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}

export default App;
