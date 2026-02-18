# Municipality Management System

A modern, full-featured ASP.NET Core 8.0 MVC web application for managing municipality operations including citizens, service requests, staff, and reports.

## Features

- **Citizen Management**: Create, read, update, and delete citizen records with validation and registration metadata
- **Service Request Tracking**: Manage citizen service requests with full CRUD and status tracking
- **Staff Directory**: Maintain a database of municipality staff with departments and positions
- **Reports Management**: Generate and track reports linked to citizens
- **Responsive UI**: Bootstrap 5 powered responsive design with centralized alerts/feedback system
- **Error Handling**: Comprehensive error handling and null-safety for models
- **Database Integration**: SQL Server with Entity Framework Core for data persistence

## Technology Stack

- **.NET Version**: ASP.NET Core 8.0
- **Database**: SQL Server (with EF Core)
- **Frontend**: Bootstrap 5, HTML5, CSS3 (custom `site.css`)
- **Architecture**: MVC (Model-View-Controller)

## Project Structure

```
MunicipalityManagementSystem/
├── Controllers/        # MVC controllers (Home, Citizens, ServiceRequests, Staff, Reports)
├── Models/             # Domain models (Citizen, ServiceRequest, Staff, Report)
├── Data/               # ApplicationDbContext and EF Core configuration
├── Views/              # Razor views
├── wwwroot/            # Static assets (css/site.css)
├── Migrations/         # EF Core migrations
├── appsettings.json    # Configuration (connection strings, logging)
└── Program.cs          # App startup, services, routing
```

## Quick Start

### Requirements

- .NET SDK 8.0 or later
- SQL Server (Express or full edition)

### Steps

1. Clone the repository and navigate into it:

```bash
git clone <repository-url>
cd MunicipalityManagementSystem
```

2. Configure your database connection in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MunicipalityDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
}
```

3. Apply EF Core migrations to create/update the database:

```bash
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

5. Open the app in your browser using the URL displayed in the console or configured in `Properties/launchSettings.json`.

## Notes & Conventions

- **Default route**: `{controller=Home}/{action=Index}/{id?}` (configured in `Program.cs`)
- Controllers use `TempData` for success/error messages; alerts are rendered in the shared layout for consistent UX
- String properties use nullable reference types (`string?`) to match null-safety conventions
- Debug console output removed in favor of UI alerts and proper logging

## Database & Migrations

EF Core migrations are tracked under `Migrations/`.

```bash
# Add a migration
dotnet ef migrations add <Name>

# Apply migration
dotnet ef database update
```

## Development Workflow

Follow coding conventions in `CONTRIBUTING.md` (indentation, async usage, commit message style).

To add a feature:
1. Add model in `Models/` and `DbSet` in `Data/ApplicationDbContext.cs`
2. Create migration and update database
3. Add controller and Razor views (manual or scaffolding)

## Troubleshooting

- **DB connection fails**: Validate `DefaultConnection` in `appsettings.json` and ensure SQL Server is running
- **Port conflicts**: Check `Properties/launchSettings.json` for configured application URLs

## Contributing

See `CONTRIBUTING.md` for contribution guidelines, code style, and pull request process.

## License

This project is released under the MIT License. See the `LICENSE` file for details.

---

*Last Updated: February 18, 2026*
