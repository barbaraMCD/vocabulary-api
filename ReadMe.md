# VocabularyAPI

API ASP.NET Core pour apprendre le vocabulaire anglais et avoir une progression utilisateur.

## Installation

1. Clone le projet
2. Assure-toi d’avoir .NET 9 installé
3. Ouvre le projet dans ton IDE préféré (VS Code, Rider)
4. Restaure les dépendances avec la commande `dotnet restore`
6. Applique les migrations avec `dotnet ef migrations add InitialCreate`
7. Applique la migration à la base de données avec `dotnet ef database update`
7. Lance l’application avec `dotnet watch run` (watch permet de le lancer avec auto reload)
8. Connectes toi à la base de données (SQLite) et vérifie que les tables sont créées (fichier de db Vocabulary.db)
8. Teste les endpoints via Postman
