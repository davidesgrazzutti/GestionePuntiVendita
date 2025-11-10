#  **Sistema Gestione Punto Vendita**

Applicazione **full-stack** per la gestione di un punto vendita:  
consente di **gestire prodotti**, **creare scontrini** e **consultare lo storico delle vendite**.

---

## ** Tecnologie**

### ** Backend**
- **.NET 9 (ASP.NET Core Web API)** → per la gestione delle API REST e della logica di business  
- **Entity Framework Core + SQLite** → per la persistenza dei dati tramite ORM leggero  
- **Swagger UI** → per la documentazione interattiva e il test diretto degli endpoint  
- **NUnit** → per l’esecuzione dei test unitari sul calcolo degli scontrini  

---

### ** Frontend**
- **React** → per la costruzione di un’interfaccia utente dinamica e reattiva  
- **Bootstrap 5** → per il layout responsivo e lo stile grafico moderno  


---

##  Come Eseguire

Prima di iniziare, assicurati di avere installato i seguenti strumenti:

* ** Git** → [https://git-scm.com/downloads](https://git-scm.com/downloads)
* ** .NET 9 SDK** → [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)
* ** Node.js + npm** → [https://nodejs.org/en/download](https://nodejs.org/en/download)
* ** (Opzionale) Visual Studio Code** → [https://code.visualstudio.com/Download](https://code.visualstudio.com/Download)
* ** (Opzionale) Visual Studio 2022** → [https://visualstudio.microsoft.com/it/vs/](https://visualstudio.microsoft.com/it/vs/)

### 1. Clonazione del Progetto

git clone [https://github.com/davidesgrazzutti/GestionePuntiVendita.git](https://github.com/davidesgrazzutti/GestionePuntiVendita.git)

### 2. Backend
  Dal terminale, posizionati nella **cartella principale GestionePuntiVendita**:
  Esegui i seguenti comandi per compilare e avviare il backend:

    dotnet build
    dotnet run

L'applicazione sarà disponibile su:
**`http://localhost:5000`**

Per accedere alla documentazione interattiva delle API (**Swagger UI**):
**`http://localhost:5000/swagger`**

> ℹ️ Il database `gestione.db` verrà creato automaticamente al primo avvio, con un seed iniziale di 4 prodotti.

### 3. Frontend
1.  Apri la cartella del progetto frontend  (che si trova all'interno della directory principale).
2.  Apri un termianle in questa cartella e poi digita i seguenti programmi:

    npm install
    npm start

Il frontend sarà visibile su:
**`http://localhost:3000`**



---

