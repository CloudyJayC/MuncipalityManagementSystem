# Municipality Management System

[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Contributions Welcome](https://img.shields.io/badge/Contributions-Welcome-brightgreen.svg)](CONTRIBUTING.md)

A modern, full-featured ASP.NET Core 10.0 MVC web application for managing municipality operations including citizens, service requests, staff, and reports.

## ✨ Features

- **Citizen Management** — Create, read, update, and delete citizen records with validation and registration metadata
- **Service Request Tracking** — Manage citizen service requests with full CRUD and status tracking
- **Staff Directory** — Maintain a database of municipality staff with departments and positions
- **Reports Management** — Generate and track reports linked to citizens
- **Responsive UI** — Bootstrap 5 powered responsive design with centralized alerts and feedback system
- **Comprehensive Error Handling** — Global exception handling, logging, and friendly error pages
- **Security Features** — Security headers (HSTS, X-Frame-Options, X-XSS-Protection), HTTPS redirection, antiforgery tokens
- **Database Integration** — SQL Server with Entity Framework Core for modern data persistence

## 🛠️ Technology Stack

| Component | Technology |
|-----------|-----------|
| **Runtime** | ASP.NET Core 10.0 |
| **Language** | C# with nullable reference types |
| **Database** | SQL Server (with Entity Framework Core 9.0) |
| **Frontend** | Bootstrap 5, HTML5, CSS3 |
| **Architecture** | MVC (Model-View-Controller) |

## 📁 Project Structure

```
MunicipalityManagementSystem/
├── Controllers/              # MVC controllers (Home, Citizens, ServiceRequests, Staff, Reports)
├── Models/                   # Domain models (Citizen, ServiceRequest, Staff, Report)
├── Data/                     # ApplicationDbContext and EF Core configuration
├── Views/
│   ├── Citizens/             # CRUD views for citizens
│   ├── ServiceRequests/       # CRUD views for service requests
│   ├── Staff/                # CRUD views for staff
│   ├── Reports/              # CRUD views for reports
│   ├── Home/                 # Home, About, Contact, Error views
│   └── Shared/               # Shared layout and partials
├── wwwroot/                  # Static assets (CSS, JS, images)
├── Migrations/               # EF Core database migrations
├── Properties/               # Launch settings and configuration
├── appsettings.json          # Application configuration
├── Program.cs                # Startup configuration, middleware, services
└── MuncipalityManagementSystem.csproj  # Project file
```

## 🚀 Quick Start

### Prerequisites

- **.NET SDK 10.0** or later ([download](https://dotnet.microsoft.com/download))
- **SQL Server** (Express or full edition)
- **Git** for version control

### Installation & Setup

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd MunicipalityManagementSystem
   ```

2. **Configure database connection**

   Edit `appsettings.json` and update the connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER\\SQLEXPRESS;Database=MuncipalityDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
   }
   ```

   Replace `YOUR_SERVER` with your SQL Server instance name.

3. **Apply Entity Framework migrations**

   ```bash
   dotnet ef database update
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

   The application will open automatically in your default browser. The default URLs are:
   - **HTTP**: `http://localhost:5284`
   - **HTTPS**: `https://localhost:7080`

## 📖 Usage

### Navigation

- **Home** — Dashboard and system overview
- **Citizens** — Manage citizen records, view registration details
- **Service Requests** — Track and manage service requests from citizens
- **Staff** — View and manage municipality staff directory
- **Reports** — Create and track reports

### Key Features

- **Validation** — All forms include client and server-side validation
- **Alerts & Feedback** — TempData-driven success/error messages displayed in alerts
- **Responsive Design** — Works seamlessly on desktop and mobile devices
- **Error Handling** — Comprehensive global exception handling with friendly error pages

## 🔧 Development

### Code Organization

- **Controllers** use dependency injection for logging and data access
- **Models** use nullable reference types (`string?`) for null-safety
- **Views** use Razor templating with Bootstrap 5 styling
- **Database** managed through EF Core migrations

### Development Workflow

1. Create a new branch: `git checkout -b feature/AmazingFeature`
2. Make your changes and test thoroughly
3. Commit with clear messages: `git commit -m 'Add AmazingFeature'`
4. Push to your branch: `git push origin feature/AmazingFeature`
5. Open a Pull Request

### Adding Database Migrations

```bash
# Create a new migration
dotnet ef migrations add <MigrationName>

# Apply migrations to database
dotnet ef database update

# Revert to previous migration
dotnet ef database update <PreviousMigrationName>
```

## 🐛 Troubleshooting

| Issue | Solution |
|-------|----------|
| **Database connection fails** | Verify connection string in `appsettings.json` and ensure SQL Server is running |
| **Port conflicts** | Check configured ports in `Properties/launchSettings.json` |
| **Migration issues** | Ensure database exists and run `dotnet ef database update` |
| **HTTPS certificate errors** | Update certificate in `Properties/launchSettings.json` or use HTTP for development |

## 🔐 Security

This application implements industry best practices for security:

- **HTTPS Redirection** — All requests redirected to HTTPS in production
- **Security Headers** — Prevents clickjacking, MIME-type sniffing, and XSS attacks
- **Antiforgery Tokens** — CSRF protection on all forms
- **Null-Safety** — Nullable reference types enabled for compile-time null checking
- **Environment-Based Configuration** — Sensitive data excluded from version control
- **Logging** — Comprehensive logging for debugging and monitoring

## 📝 Contributing

We welcome contributions! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for:
- Code of conduct
- How to report bugs
- How to suggest enhancements
- Pull request process
- Coding standards and style guides

## 📜 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for complete details.

## 📧 Support

If you encounter issues or have questions:
- Check the [Troubleshooting](#-troubleshooting) section
- Review [CONTRIBUTING.md](CONTRIBUTING.md)
- Open an issue on GitHub with detailed information

## 🙏 Acknowledgments

- Built with [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- UI powered by [Bootstrap 5](https://getbootstrap.com/)
- Icons from [Bootstrap Icons](https://icons.getbootstrap.com/)

---

**Last Updated**: February 24, 2026  
**Version**: 1.0.0
