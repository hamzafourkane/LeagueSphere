# ⚽ LeagueSphere

[![.NET Core](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4?logo=nuget&logoColor=white)](https://docs.microsoft.com/ef/core/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC292B?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

**LeagueSphere** est une plateforme web moderne sous **ASP.NET Core MVC** qui combine la **gestion administrative de championnats de football** et un **réseau social interactif pour supporters avec pronostics en temps réel**.

---

## 🌟 Fonctionnalités Clés

### 🛡️ 1. Gestion de Ligue & Championnat
- **Gestion des Équipes** : Ajout, modification, logos personnalisés et suivi des performances (victoires, nuls, défaites, buts pour/contre).
- **Calendrier & Feuilles de Match** : Planification des rencontres et saisie des scores officiels.
- **Classement Automatisé** : Recalcul dynamique de l'ensemble du classement dès qu'un match est marqué comme terminé, respectant les règles officielles (Points $\rightarrow$ Différence de buts $\rightarrow$ Meilleure attaque).
- **Export PDF / Impression** : Feuille de style dédiée (`@media print`) permettant d'imprimer ou d'exporter un classement propre en un clic.

### 📊 2. Dashboard Administrateur
- Vue analytique exclusive pour les administrateurs avec indicateurs clés :
  - Nombre de clubs engagés
  - Total des buts inscrits
  - Jauge de progression du championnat (%)
  - Carte d'honneur du leader actuel
  - Tableau récapitulatif des derniers résultats et raccourcis rapides

### 📢 3. Réseau Social & Espace Communauté
- **Fil d'Actualité** : Publications d'articles, actualités et photos avec horodatage.
- **Commentaires & Échanges** : Fils de discussion sous chaque publication.
- **Système de Mention "J'aime"** : Fonctionnalité toggle (Like/Unlike) persistée par utilisateur.

### 🔥 4. Pronostics en Direct (1 / N / 2)
- Les supporters peuvent voter sur l'issue des matchs à venir (Victoire Domicile, Match Nul, Victoire Extérieur).
- Calcul et affichage en temps réel des jauges de pourcentages et du nombre total de votants.

### 🔐 5. Sécurité & Authentification
- Intégration complète de **Microsoft ASP.NET Core Identity**.
- Gestion des rôles : `Admin` et `User`.
- Seeding automatique au démarrage : création des rôles et d'un compte Administrateur prêt à l'emploi.

---

## 🛠️ Stack Technologique

- **Framework** : ASP.NET Core MVC (.NET 10)
- **Langage** : C#
- **ORM** : Entity Framework Core 10 (Code-First)
- **Base de données** : Microsoft SQL Server / LocalDB
- **Frontend** : Razor Views, HTML5, CSS3 personnalisé, Bootstrap 5, JavaScript Vanilla
- **Sécurité** : ASP.NET Core Identity & Anti-Forgery Tokens

---

## 📁 Structure du Projet

```text
LeagueSphere/
├── Controllers/              # Contrôleurs MVC (Home, Matchs, Equipes, Posts, Classement)
├── Data/                     # DbContext, Seeder et Migrations EF Core
│   ├── ApplicationDbContext.cs
│   └── DbSeeder.cs
├── Models/                   # Entités et ViewModels
│   ├── Commentaire.cs
│   ├── Equipe.cs
│   ├── FeedViewModel.cs
│   ├── Match.cs
│   ├── Post.cs
│   └── PostLike.cs
├── Views/                    # Vues Razor organisées par domaine
│   ├── Classement/
│   ├── Equipes/
│   ├── Home/                 # Accueil (Feed public & Dashboard Admin)
│   ├── Matchs/
│   ├── Posts/
│   └── Shared/               # Layout, Dark Mode & Partials
├── wwwroot/                  # Fichiers statiques (CSS, JS, Bibliothèques)
├── appsettings.json          # Configuration et chaîne de connexion
├── LeagueSphere.csproj       # Définition du projet .NET
└── LeagueSphere.slnx         # Solution Visual Studio
```

---

## 🚀 Démarrage Rapide

### Prérequis
- [.NET SDK 10.0](https://dotnet.microsoft.com/) ou version compatible
- [Visual Studio 2022 / 2025](https://visualstudio.microsoft.com/) (avec la charge de travail *ASP.NET et développement web*) ou Visual Studio Code
- [SQL Server Express / LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)

### Installation

1. **Cloner le dépôt :**
   ```bash
   git clone https://github.com/votre-nom/LeagueSphere.git
   cd LeagueSphere
   ```

2. **Configurer la chaîne de connexion :**
   Vérifiez ou adaptez la chaîne de connexion dans `appsettings.json` :
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LeagueSphereDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

3. **Appliquer les migrations :**
   ```bash
   dotnet ef database update
   ```

4. **Lancer l'application :**
   ```bash
   dotnet run
   ```
   L'application sera accessible sur `https://localhost:7xxx` ou `http://localhost:5xxx`.

---

## 🔑 Identifiants par Défaut (Seed)

Au premier lancement, un administrateur est créé automatiquement via `DbSeeder` :
- **Email** : `admin@leaguesphere.com`
- **Mot de passe** : `Admin123!`
- **Rôle** : `Admin`

---


## 📄 Licence

Ce projet est sous licence [MIT](LICENSE). Libre d'utilisation et d'adaptation.
