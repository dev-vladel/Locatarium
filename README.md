# Locatarium
Locatarium features an online platform for renting flats, representing my actual bachelor degree.

## Installation

Make sure you have the following installed:
- **[.NET Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2)**
- **[Microsoft SQL Server](https://www.microsoft.com/en-us/download/details.aspx?id=101064)** (I recommend 2017 or higher, and make sure you remember the master user credentials after installation)

## Usage
- Open project
- Update *appsettings.json* from **Web** project config file substituting the first variables to your server, database, username and password (*from SQL Server install*):

```
"ConnectionStrings": {
    "LocatariumDb": "Server=server;Database=database;User=user;Password=password;Trusted_Connection=False;Encrypt=True;Connection Timeout=60;TrustServerCertificate=True"
  }
```

- Update same *connection string* from **Designer** project inside **DBContextFactory** class as previous step
- Open a **command prompt** or **Package Manager Console** from Visual Studio inside the **Designer** project and run the following commands:

```
dotnet ef
dotnet ef migrations add
dotnet ef database update
```

- Open **Visual Sutdio** and build project (set **Web** as startup project)
