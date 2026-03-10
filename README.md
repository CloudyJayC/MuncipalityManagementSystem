# Municipality Management System

[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/download)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![EF Core](https://img.shields.io/badge/EF_Core-10.0-orange)](https://docs.microsoft.com/en-us/ef/core/)
[![SignalR](https://img.shields.io/badge/SignalR-Real--time-green)](https://dotnet.microsoft.com/apps/aspnet/signalr)

An ASP.NET Core 10.0 MVC web application for managing local municipality operations. Provides role-based access for administrators, staff, and citizens to manage service requests, citizen records, staff, and reports — with real-time notifications powered by SignalR.

---

## Features

- **Authentication** — Secure login and registration with ASP.NET Core Identity, account lockout on failed attempts
- **Role-Based Access** — Admin, Staff, and Citizen roles with permissions enforced server-side on every action
- **Citizen Management** — Create, edit, and manage citizen records with full address and contact details
- **Service Requests** — Submit, track, and manage service requests with status updates (Pending, In Progress, Completed, Cancelled)
- **Citizen Portal** — Citizens can view their own requests, cancel pending requests, update their contact details, and delete their account
- **Real-time Notifications** — Citizens receive live bell badge updates and toast popups via SignalR when their service request status changes, without requiring a page refresh
- **Notifications Page** — Full notifications management for citizens: view, mark read, mark all read, delete, with links back to the related service request
- **Staff Portal** — Staff dashboard with live stats and recent requests, restricted permissions enforced server-side and in views
- **Staff Directory** — Maintain staff records with department and position information, linked to login accounts
- **Reports** — Create and manage reports linked to citizen records
- **Admin Portal** — Admin dashboard with system-wide stats, full user management, and staff account creation
- **Security** — HTTPS, security headers, CSRF protection on all forms, login lockout, and environment-based configuration
- **Error Handling** — Global exception handling with user-friendly error pages and structured logging

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10.0 (MVC) |
| Language | C# with nullable reference types |
| Auth | ASP.NET Core Identity |
| Database | SQL Server + Entity Framework Core 10.0 |
| Real-time | ASP.NET Core SignalR |
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
├── Hubs/                   # SignalR hubs
├── Migrations/             # EF Core migrations
├── Models/                 # Domain models + ApplicationUser
├── Services/               # NotificationService, EmailSender
├── Views/                  # Razor views
│   ├── Admin/              # Admin dashboard and user management
│   ├── CitizenPortal/      # Citizen profile and account management
│   ├── Citizens/           # Citizen CRUD views
│   ├── Notifications/      # Citizen notifications page
│   ├── ServiceRequests/    # Service request views
│   ├── Reports/            # Report views
│   ├── Staff/              # Staff views
│   ├── StaffPortal/        # Staff dashboard
│   └── Shared/             # Layout and partials
├── wwwroot/                # Static files (CSS, JS)
├── Properties/             # Launch settings
├── Program.cs              # App startup and middleware
└── appsettings.json        # Configuration (connection string placeholder)
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

6. Default seeded accounts created automatically on startup:
   - **Admin:** `admin@municipality.com` / `Admin1234`
   - **Staff:** `staff@municipality.com` / `Staff1234!`

---

## Roles

| Role | Access |
|---|---|
| Admin | Full access — manages users, citizens, staff, service requests, reports |
| Staff | Views citizens, manages service request statuses and reports, views staff directory |
| Citizen | Submits and tracks their own service requests, receives real-time notifications, manages their own profile |

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

### Notifications
| Action | Admin | Staff | Citizen |
|---|---|---|---|
| View | ❌ | ❌ | ✅ Own only |
| Mark Read / Mark All Read | ❌ | ❌ | ✅ Own only |
| Delete | ❌ | ❌ | ✅ Own only |

### User Management (Admin only)
| Action | Admin |
|---|---|
| View all users | ✅ |
| Create Staff account | ✅ |
| Delete user account | ✅ |
| Delete own account | ❌ |

---

## Usage

**Citizens** can register publicly, submit service requests, cancel pending requests, update their contact details, and delete their account from their personal portal. When a staff member or admin updates the status of their service request, they receive a live toast notification and a bell badge update in the navbar without needing to refresh the page. All notifications are stored and accessible from the Notifications page.

**Staff** can view all citizens, update service request statuses (which automatically triggers citizen notifications), manage reports, and view the staff directory. Staff land on a dashboard showing live stats and the 5 most recent service requests on login.

**Admins** have full access to all modules. They land on a dashboard with system-wide stats on login, can manage all user accounts, create new staff logins (which automatically creates the linked Staff directory entry), and delete user accounts with clean data detachment across all linked records.

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
| `appsettings.json` | Base configuration (connection string placeholder only) |
| `appsettings.Development.json` | Local overrides — excluded from git |

---

## Security

- HTTPS redirection in production
- Security headers (X-Frame-Options, X-Content-Type-Options, Referrer-Policy)
- CSRF tokens on all state-changing forms
- ASP.NET Core Identity password hashing and account lockout
- Role-based authorization enforced server-side on every controller action
- UserId preserved on all edit operations to prevent data detachment
- Admin cannot delete their own account
- UserId safely detached from all linked records before account deletion
- No credentials committed to version control
- SignalR hub protected with `[Authorize]` — unauthenticated connections rejected

---

## Troubleshooting

**Database connection fails**
Verify your connection string in `appsettings.Development.json` and confirm SQL Server is running.

**Port already in use**
Update the ports in `Properties/launchSettings.json`.

**Migration errors**
Check if a previous migration needs to be reverted first, or drop the database and re-run `dotnet ef database update`.

**Login redirects to 404**
Ensure `app.MapRazorPages()` is present in `Program.cs` after `app.MapControllerRoute()`.

**SignalR not connecting**
Ensure you are logged in — the hub requires authentication. Check the browser console for `"SignalR connected."` on Citizen accounts.

---

## Roadmap

- [x] Full CRUD for Citizens, Service Requests, Staff, Reports
- [x] ASP.NET Core Identity authentication with account lockout
- [x] Role-based access (Admin, Staff, Citizen) enforced server-side
- [x] Citizen portal — profile management, cancel requests, delete account
- [x] Role-aware navbar and views
- [x] Staff portal — dashboard, restricted permissions, login redirect
- [x] Admin portal — user management, create staff, login redirect
- [x] Real-time notifications via SignalR — bell badge, live toasts, notifications page
- [ ] Frontend overhaul — AdminLTE 3, Chart.js dashboards, DataTables, SweetAlert2
- [ ] Live deployment to Azure

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on reporting bugs, suggesting features, and submitting pull requests.

---

## License

MIT License — see [LICENSE](LICENSE) for details.

---

**Version**: 0.6.0 — Notifications + SignalR (pre-release)
**Last Updated**: March 2026
