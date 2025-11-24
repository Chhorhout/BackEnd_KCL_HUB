AMS — Asset Management System (ASP.NET Core Web API)
===================================================

Badges
------

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91)
![EF Core](https://img.shields.io/badge/Entity%20Framework-Core-2D9CDB)
![AutoMapper](https://img.shields.io/badge/AutoMapper-enabled-FF6F00)

Overview
--------

AMS is a production-ready Asset Management System built with ASP.NET Core Web API and Entity Framework Core. It provides end‑to‑end management for assets, owners, locations, suppliers, maintenance, temporary usage, and related status history.

This repository contains the API service (`AMS.Api`) with a clean separation of concerns across Controllers, Data (DbContext, configurations, migrations), Models, DTOs, and mapping profiles.

Author
------

Created and maintained by **Chhorhout**.

Key Features
------------

- Full CRUD for assets and related domain entities
- Strongly-typed DTOs and AutoMapper mapping profiles
- EF Core code‑first with granular entity configurations
- Transaction-safe status and ownership history tracking
- Swagger/OpenAPI out of the box for interactive docs
- Environment-specific configuration via appsettings files

Tech Stack
---------

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (Code‑First Migrations)
- AutoMapper
- SQL Server (or any EF Core supported RDBMS)

Repository Structure
--------------------

```
AMS/
├─ AMS.Api/
│  ├─ Controller/              # REST controllers per aggregate
│  ├─ Data/
│  │  ├─ ApplicationDbcontext.cs
│  │  ├─ Configs/              # Fluent EF Core entity configurations
│  │  └─ Migrations/           # Code‑first migration history
│  ├─ Dtos/                    # Request/response DTOs
│  ├─ Mapping/                 # AutoMapper profiles
│  ├─ Models/                  # Domain entities
│  ├─ Program.cs               # Web host & service wiring
│  ├─ appsettings*.json        # Configuration (per environment)
│  └─ MyFirstWebApi.csproj
└─ AMS.sln
```

Getting Started
---------------

Prerequisites
-------------

- .NET 8 SDK (`dotnet --version` >= 8.x)
- SQL Server (Developer/Express/LocalDB) or another EF Core-supported DB
- Git (optional, for source control)

Configuration
-------------

1. Duplicate `AMS.Api/appsettings.json` into `AMS.Api/appsettings.Development.json` (if not present).
2. Set your connection string under `ConnectionStrings:DefaultConnection`.

Example (SQL Server LocalDB):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AMS;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

Database Setup
--------------

Apply migrations to create/update the database schema.

```
# If dotnet-ef is not installed
dotnet tool install --global dotnet-ef

# From the repo root or within AMS.Api
dotnet ef database update --project AMS.Api
```

Common EF Core commands:

```
# Add a new migration
dotnet ef migrations add <MigrationName> --project AMS.Api

# Update db to latest
dotnet ef database update --project AMS.Api
```

Run the API
-----------

```
# From the repository root
dotnet run --project AMS.Api
```

By default, Swagger UI will be available at:

- Development: https://localhost:<port>/swagger

Replace `<port>` with the HTTPS port printed on application startup.

API Surface (High-Level)
------------------------

The API follows standard REST semantics. The following aggregates have dedicated controllers under `AMS.Api/Controller`:

- Assets, Asset Types, Asset Status, Asset Status History
- Owners, Owner Types, Asset Ownerships
- Locations, Suppliers
- Maintainers, Maintenance Records, Maintenance Parts
- Temporary Users, Temporary Used Requests, Temporary Used Records

Each controller typically provides CRUD endpoints under `/api/<Resource>`.

Example requests (HTTP file)
----------------------------

You can explore requests using the included HTTP file `AMS.Api/MyFirstWebApi.http` in IDEs like VS Code or Rider, or interact via Swagger UI.

Automapper & DTOs
-----------------

Mappings between domain entities and DTOs are defined in `AMS.Api/Mapping/MappingProfile.cs`. Create/update payloads live in `AMS.Api/Dtos/*CreateDto.cs`; responses use `*ResponseDto.cs`.

Configuration-by-Convention
---------------------------

Entity configurations are organized per model under `AMS.Api/Data/Configs`. This keeps the `DbContext` lean and ensures consistent constraints, relationships, and indexes across the schema.

Environment Variables
---------------------

The app uses ASP.NET Core configuration binding. You can override settings with environment variables using `:` separators, e.g.:

```
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings:DefaultConnection="<your-connection-string>"
```

Quality & Conventions
---------------------

- DTOs isolate the API contract from entity persistence models
- Controller actions validate input and return proper HTTP status codes
- Migrations represent the single source of truth for database evolution

Troubleshooting
---------------

- Connection errors: verify your `DefaultConnection` string and that SQL Server is reachable.
- `dotnet-ef` not found: install the global tool as shown above.
- Swagger not loading: ensure you are running in Development and the app started successfully.

Contributing
------------

Contributions are welcome! Please open an issue or submit a pull request. For larger changes, start a discussion first to align on scope and approach.

License
-------

Specify your preferred license (e.g., MIT) and add a `LICENSE` file. If omitted, all rights are reserved by default.

Acknowledgements
----------------

- Thanks to the .NET and EF Core communities for libraries and guidance.
- Project created by **Chhorhout**.


