# Municipality Management System

[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/download)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![EF Core](https://img.shields.io/badge/EF_Core-10.0-orange)](https://docs.microsoft.com/en-us/ef/core/)

An ASP.NET Core 10.0 MVC web application for managing local municipality operations. Provides role-based access for administrators, staff, and citizens to manage service requests, citizen records, staff, and reports.

---

## Features

- **Authentication** — Secure login and registration with ASP.NET Core Identity
- **Role-Based Access** — Admin, Staff, and Citizen roles with appropriate permissions
- **Citizen Management** — Create, edit, and manage citizen records with contact details
- **Service Requests** — Submit, track, and manage service requests with status updates
- **Citizen Portal** — Citizens can view their own requests, cancel pending requests, update their profile, and delete their account
- **Staff Portal** — Staff dashboard with stats overview, restricted permissions enforced server-side and in views
- **Staff Directory** — Maintain staff records with department and position information
- **Reports** — Create and manage reports linked to citizen records
- **Security** — HTTPS, security headers, CSRF protection, and environment-based configuration
- **Error Handling** — Global exception handling with user-friendly error pages and logging

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10.0 (MVC) |
| Language | C# with nullable reference types |
| Auth | ASP.NET Core Identity |
| Database | SQL Server + Entity Framework Core 10.0 |
| Frontend | Bootstrap 5, HTML5, CSS3 |
| Icons | Bootstrap Icons |

---

## Project Structure

```
MunicipalityManagementSystem/
├── Areas/
│   └── Identity/           # Scaffolded Identity UI (Login, Register, Logout)
├── Controllers/            # MVC controllers
├── Data/                   # DbContext and database configuration
├── Migrations/             # EF Core migrations
├── Models/                 # Domain models + ApplicationUser
├── Services/               # Email sender service
├── Views/                  # Razor views
│   ├── CitizenPortal/      # Citizen profile and account management
│   ├── Citizens/           # Citizen CRUD views
│   ├── ServiceRequests/    # Service request views
│   ├── Reports/            # Report views
│   ├── Staff/              # Staff views
│   ├── StaffPortal/        # Staff dashboard
│   └── Shared/             # Layout and partials
├── wwwroot/                # Static files (CSS, JS)
├── Properties/             # Launch settings
├── Program.cs              # App startup and middleware
└── appsettings.json        # Configuration (connection string)
```

---

## Getting Started

### Requirements

- .NET SDK 10.0 or later
- SQL Server (Express or full version)
- Git

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/CloudyJayC/MuncipalityManagementSystem.git
   cd MuncipalityManagementSystem
   ```

2. Add your connection string. Create `appsettings.Development.json` in the root (this file is excluded from git):

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER\\SQLEXPRESS;Database=MunicipalityDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
     }
   }
   ```

   Replace `YOUR_SERVER` with your SQL Server instance name.

3. Apply migrations to create the database:

   ```bash
   dotnet ef database update
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

5. Open your browser:
   - HTTPS: `https://localhost:7080`
   - HTTP: `http://localhost:5284`

6. A default Admin account is seeded automatically on startup:
   - **Email:** `admin@municipality.com`
   - **Password:** `Admin1234`

---

## Roles

| Role | Access |
|---|---|
| Admin | Full access — manages users, citizens, staff, service requests, reports |
| Staff | Views citizens, manages service requests and reports, views staff directory |
| Citizen | Submits and tracks their own service requests, manages their own profile |

---

## Role Permissions

### Citizens
| Action | Admin | Staff | Citizen |
|---|---|---|---|
| View | ✅ | ✅ | ❌ |
| Create | ✅ | ❌ | ❌ |
| Edit | ✅ | ✅ | ❌ |
| Delete | ✅ | ❌ | ❌ |

### Service Requests
| Action | Admin | Staff | Citizen |
|---|---|---|---|
| View | ✅ All | ✅ All | ✅ Own only |
| Create | ✅ | ✅ | ✅ |
| Edit / Update Status | ✅ | ✅ | ❌ |
| Cancel | ❌ | ❌ | ✅ Pending only |
| Delete | ✅ | ❌ | ❌ |

### Reports
| Action | Admin | Staff | Citizen |
|---|---|---|---|
| View | ✅ | ✅ | ❌ |
| Create | ✅ | ✅ | ❌ |
| Edit | ✅ | ✅ | ❌ |
| Delete | ✅ | ❌ | ❌ |

### Staff Directory
| Action | Admin | Staff | Citizen |
|---|---|---|---|
| View | ✅ | ✅ | ❌ |
| Create | ✅ | ❌ | ❌ |
| Edit | ✅ | ❌ | ❌ |
| Delete | ✅ | ❌ | ❌ |

---

## Usage

**Citizens** can register publicly, submit service requests, cancel pending requests, update their contact details, and delete their account from their personal portal.

**Staff** can view all citizens, update service request statuses, manage reports, and view the staff directory. Staff land on a dashboard showing live stats and recent requests on login.

**Admins** have full access to all modules and can manage user accounts and roles.

---

## Development

### Adding Features

1. Create a feature branch from master
2. Add models to `Models/` and update `ApplicationDbContext.cs`
3. Create a migration: `dotnet ef migrations add YourMigrationName`
4. Apply the migration: `dotnet ef database update`
5. Add controllers and views as needed
6. Test thoroughly before merging

### Database Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Rollback to a previous migration
dotnet ef database update PreviousMigrationName
```

---

## Configuration

The app uses environment-specific settings:

- **Development** — Detailed logging, developer exception pages
- **Production** — Error handler, HSTS, security headers

| File | Purpose |
|---|---|
| `appsettings.json` | Base configuration |
| `appsettings.Development.json` | Local overrides — excluded from git |

---

## Security

- HTTPS redirection in production
- Security headers (X-Frame-Options, X-Content-Type-Options, Referrer-Policy)
- CSRF tokens on all forms
- ASP.NET Core Identity password hashing
- Role-based authorization enforced both server-side and in views
- UserId preserved on all edit operations to prevent data detachment
- No credentials committed to version control

---

## Troubleshooting

**Database connection fails**
Verify your connection string and confirm SQL Server is running.

**Port already in use**
Update the ports in `Properties/launchSettings.json`.

**Migration errors**
Check if a previous migration needs to be reverted first, or drop the database and re-run `dotnet ef database update`.

**Login redirects to 404**
Ensure `app.MapRazorPages()` is present in `Program.cs` after `app.MapControllerRoute()`.

---

## Roadmap

- [x] Full CRUD for Citizens, Service Requests, Staff, Reports
- [x] ASP.NET Core Identity authentication
- [x] Role-based access (Admin, Staff, Citizen)
- [x] Citizen portal — profile management, cancel requests, delete account
- [x] Role-aware navbar and views
- [x] Staff portal — dashboard, restricted permissions, login redirect
- [ ] Admin portal — user management, role assignment
- [ ] Service request status notifications (SignalR)
- [ ] Live deployment to Azure with Supabase PostgreSQL

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on reporting bugs, suggesting features, and submitting pull requests.

---

## License

MIT License — see [LICENSE](LICENSE) for details.

---

**Version**: 1.3.0 — Staff Portal
**Last Updated**: March 2026