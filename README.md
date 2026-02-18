# Municipality Management System

A modern, full-featured ASP.NET Core web application for managing municipality operations including citizens, service requests, staff, and reports.

## Features

- **Citizen Management**: Create, read, update, and delete citizen records with comprehensive details
- **Service Request Tracking**: Manage citizen service requests with status tracking
- **Staff Directory**: Maintain a database of municipality staff with departments and positions
- **Reports Management**: Generate and track reports related to citizens
- **Responsive UI**: Bootstrap 5 powered responsive design that works on all devices
- **Error Handling**: Comprehensive error handling with user-friendly messages
- **Database Integration**: SQL Server with Entity Framework Core for data persistence

## Technology Stack

- **.NET Version**: ASP.NET Core 8.0
- **Database**: SQL Server with Entity Framework Core
- **Frontend**: Bootstrap 5, HTML5, CSS3
- **Architecture**: MVC (Model-View-Controller)

## Project Structure

```
MuncipalityManagementSystem/
├── Controllers/          # MVC Controllers for business logic
│   ├── HomeController.cs
│   ├── CitizensController.cs
# Municipality Management System

A concise, up-to-date overview and setup guide for the Municipality Management System web application.

This repository is an ASP.NET Core MVC application that helps manage citizens, service requests, staff, and reports for a municipality.

## Key Features

- Citizen CRUD with validation and registration metadata
- Service Request tracking with full CRUD and status management (Delete implemented)
- Staff directory with department and position tracking
- Report creation and tracking linked to citizens
- Responsive UI (Bootstrap 5) with centralized alert/feedback system (TempData + Bootstrap alerts)
- EF Core (SQL Server) for persistence and migrations
- Improved error handling, null-safety for models, and consistent code style

## Technology

- Target framework: .NET 8.0 (use .NET SDK 8.0 or later)
- ASP.NET Core MVC
- Entity Framework Core with SQL Server
- Frontend: Bootstrap 5, custom wwwroot/css/site.css

## Project layout (important files)

```
./
├─ Controllers/            # MVC controllers (Home, Citizens, ServiceRequests, Staff, Reports)
├─ Models/                 # Domain models (Citizen, ServiceRequest, Staff, Report)
├─ Data/                   # ApplicationDbContext and EF Core configuration
├─ Views/                  # Razor views
├─ wwwroot/                # Static assets (css/site.css)
├─ Migrations/             # EF Core migrations
├─ appsettings.json        # Configuration (connection strings, logging)
└─ Program.cs              # App startup, services, routing
```

## Quick Start

Requirements:

- .NET SDK 8.0 or later
- SQL Server (Express or full)

1. Clone the repo and change directory:

```bash
git clone <repository-url>
cd MuncipalityManagementSystem
```

2. Configure the database connection in `appsettings.json` (edit `DefaultConnection`):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MuncipalityDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
}
```

3. Apply EF Core migrations to create/update the database:

```bash
dotnet ef database update
```

4. Run the app:

```bash
dotnet run
```

Open the app using the URL shown in the console or configured in `Properties/launchSettings.json`.

## Notes on current behavior and conventions

- Default route points to `HomeController` (`{controller=Home}/{action=Index}/{id?}`) — see `Program.cs`.
- Controllers use `TempData` for success/error messages; alerts are rendered in the shared layout for consistent UX.
- String properties in models use nullable reference types (e.g., `string?`) to match project null-safety decisions.
- Debug console output was removed in favor of UI alerts and proper logging.

## Database & Migrations

- Migrations are tracked under `Migrations/`. Use `dotnet ef migrations add <Name>` to create a migration, then `dotnet ef database update`.

## Development workflow

- Follow the coding conventions in `CONTRIBUTING.md` (indentation, async usage, commit message style).
- To add a feature:
  - Add model to `Models/` and DbSet to `Data/ApplicationDbContext.cs`.
  - Create a migration and update the database.
  - Add controller and Razor views (scaffolding or manual).

## Troubleshooting

- If the DB connection fails: validate `DefaultConnection` in `appsettings.json` and ensure SQL Server is running.
- If ports conflict: check `Properties/launchSettings.json` for configured application URLs.

## Contributing

See `CONTRIBUTING.md` for contribution guidelines, code style, and PR process.

## License

This project is released under the MIT License. See the `LICENSE` file for details.

---

**Last Updated**: February 18, 2026

## License

This project is released under the MIT License. See the `LICENSE` file for details.

---

**Last Updated**: February 18, 2026