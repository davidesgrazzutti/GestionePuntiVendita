#  **Sistema Gestione Punto Vendita**

Applicazione **full-stack** per la gestione di un punto vendita:  
consente di **gestire prodotti**, **creare scontrini** e **consultare lo storico delle vendite**.

---

## **Tecnologie**

### **Backend**
- **.NET 9 (ASP.NET Core Web API)** 
- **Entity Framework Core + SQLite**   
- **Swagger UI**   
- **NUnit**   

---

### **Frontend**
- **React**   
- **Bootstrap 5**   


---

##  Come Eseguire

Prima di iniziare, assicurati di avere installato i seguenti strumenti:

* ** Git** → [https://git-scm.com/downloads](https://git-scm.com/downloads)
* ** .NET 9 SDK** → [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)
* ** Node.js + npm** → [https://nodejs.org/en/download](https://nodejs.org/en/download)


### 1. Clonazione del Progetto

```bash
git clone https://github.com/davidesgrazzutti/GestionePuntiVendita.git
```

### 2. Backend
  Dal terminale, posizionati nella **cartella principale GestionePuntiVendita**:
  
  Esegui i seguenti comandi per compilare e avviare il backend:

    dotnet build
    dotnet run


Per accedere alla documentazione interattiva delle API (**Swagger UI**):
**`http://localhost:5000/swagger`**

> ℹ️ Il database `gestione.db` verrà creato automaticamente al primo avvio, con un seed iniziale di 4 prodotti.

### 3. Frontend
1.  Posizionati nella cartella frontend  che si trova all'interno della directory principale.
2.  Apri un terminale in questa cartella e poi digita i seguenti programmi:

    ```bash
    npm install
    npm start
    ```

Il frontend sarà visibile su:
**`http://localhost:3000`**

---

###  **Autenticazione e Autorizzazione**
- Attualmente l’applicazione non richiede login.  
- Si potrebbe integrare un sistema di autenticazione **JWT (JSON Web Token)**.
- Ogni utente potrebbe avere un proprio storico scontrini.

---

###  **Gestione Avanzata dei Prodotti**
- Aggiunta di endpoint **POST / PUT / DELETE** per creare, aggiornare o rimuovere prodotti.  
- Validazione per evitare **prodotti duplicati** (es. stesso SKU già presente).  





---

