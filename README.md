# Municipality Management System

[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

An ASP.NET Core 10.0 MVC web application for managing municipality operations. Tracks citizens, service requests, staff, and reports.

## Features

- **Citizen Management** - Create, edit, and manage citizen records with contact details and registration dates
- **Service Requests** - Track service requests with status updates and citizen assignments
- **Staff Directory** - Maintain staff records with department and position information
- **Reports** - Generate and manage reports linked to citizens
- **Error Handling** - Global exception handling with user-friendly error pages and logging
- **Security** - HTTPS redirection, security headers, CSRF protection, and environment-based configuration

## Tech Stack

- **Framework**: ASP.NET Core 10.0 (MVC)
- **Language**: C# (nullable reference types enabled)
- **Database**: SQL Server with Entity Framework Core 9.0
- **Frontend**: Bootstrap 5, HTML5, CSS3
- **Icons**: Bootstrap Icons

## Project Structure

```
MunicipalityManagementSystem/
├── Controllers/        # MVC controllers
├── Models/            # Domain models
├── Data/              # DbContext and database configuration
├── Views/             # Razor views
├── Migrations/        # EF Core migrations
├── wwwroot/           # Static files (CSS, JS, images)
├── Properties/        # Launch settings
├── Program.cs         # App startup and middleware
└── appsettings.json   # Configuration
```

## Getting Started

### Requirements

- .NET SDK 10.0 or later
- SQL Server (Express or full version)
- Git

### Setup

1. Clone the repository:

   ```bash
   git clone <repository-url>
   cd MunicipalityManagementSystem
   ```

2. Update the database connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER\\SQLEXPRESS;Database=MuncipalityDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
   }
   ```

   Replace `YOUR_SERVER` with your SQL Server instance name.

3. Run migrations to create the database:

   ```bash
   dotnet ef database update
   ```

4. Start the application:

   ```bash
   dotnet run
   ```

5. Open your browser and navigate to:
   - HTTPS: `https://localhost:7080`
   - HTTP: `http://localhost:5284`

## Usage

The app has five main sections accessible from the navigation bar:

- **Home** - Landing page with overview
- **Citizens** - Manage citizen records
- **Service Requests** - Track requests with status and assignment
- **Staff** - View and edit staff directory
- **Reports** - Create and view reports

All forms include validation. Success and error messages appear as dismissible alerts at the top of each page.

## Development

### Adding Features

1. Fork the repo and create a feature branch
2. Add your models to `Models/` and update `ApplicationDbContext.cs`
3. Create a migration: `dotnet ef migrations add YourMigrationName`
4. Apply the migration: `dotnet ef database update`
5. Add controllers and views as needed
6. Test your changes
7. Submit a pull request

### Database Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Rollback to a specific migration
dotnet ef database update PreviousMigrationName
```

## Configuration

The app uses environment-specific settings:

- **Development**: Detailed logging, developer exception pages
- **Production**: Error handler, HSTS, security headers

Configuration files are in the root directory:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development overrides (excluded from git)

## Security Notes

This project follows standard security practices:

- HTTPS redirection in production
- Security headers (X-Frame-Options, X-Content-Type-Options, etc.)
- CSRF tokens on forms
- Logging for debugging and audit trails
- Sensitive configuration excluded from version control

**Before committing**: Make sure `appsettings.Development.json` contains no sensitive data. The `.gitignore` file already excludes it.

## Troubleshooting

**Database connection fails**  
Check your connection string in `appsettings.json` and verify SQL Server is running.

**Port already in use**  
Change the ports in `Properties/launchSettings.json`.

**Migration errors**  
Delete the database and run `dotnet ef database update` again. Or check if previous migrations need to be reverted first.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on:
- Reporting bugs
- Suggesting features
- Submitting pull requests
- Code style and standards

## License

MIT License - see [LICENSE](LICENSE) file for details.

---

**Version**: 1.0.0  
**Last Updated**: February 24, 2026
