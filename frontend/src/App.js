import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

import React, { useEffect, useState } from "react";

const API_BASE = "http://localhost:5000"; // URL backend .NET

function App() {
  const [products, setProducts] = useState([]);
  const [ticketItems, setTicketItems] = useState([]);
  const [total, setTotal] = useState(0);

  // carica prodotti
  useEffect(() => {
    fetch(`${API_BASE}/api/products`)
      .then((res) => res.json())
      .then((data) => setProducts(data))
      .catch(() => alert("Errore nel caricamento dei prodotti"));
  }, []);

  // aggiungi prodotto allo scontrino
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

  // calcola totale
  const calculateTotal = (items) => {
    const sum = items.reduce((acc, i) => acc + i.unitPrice * i.quantity, 0);
    setTotal(sum.toFixed(2));
  };

  // invia ticket
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
      })
      .catch(() => alert("Errore durante la creazione dello scontrino"));
  };

  return (
    <div className="container py-4">
      <h1 className="text-center mb-4">Gestione Punto Vendita</h1>

      {/* 🛒 Prodotti in card */}
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
                className="btn btn-primary mt-2"
                onClick={() => addToTicket(p)}
              >
                ➕ Aggiungi
              </button>
            </div>
          </div>
        ))}
      </div>

      {/* 🧾 Scontrino */}
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
                  <th>Quantità</th>
                  <th>Prezzo Unitario</th>
                  <th>Subtotale</th>
                </tr>
              </thead>
              <tbody>
                {ticketItems.map((i) => (
                  <tr key={i.productId}>
                    <td>{i.productName}</td>
                    <td>{i.quantity}</td>
                    <td>€ {i.unitPrice.toFixed(2)}</td>
                    <td>€ {(i.quantity * i.unitPrice).toFixed(2)}</td>
                  </tr>
                ))}
              </tbody>
              <tfoot>
                <tr>
                  <th colSpan="3" className="text-end">
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
    </div>
  );
}

export default App;
