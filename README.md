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
│   ├── ServiceRequestsController.cs
│   ├── StaffController.cs
│   └── ReportsController.cs
├── Models/              # Data models
│   ├── Citizen.cs
│   ├── ServiceRequest.cs
│   ├── Staff.cs
│   └── Report.cs
├── Data/                # Database context
│   └── ApplicationDbContext.cs
├── Views/               # Razor views for each controller
│   ├── Home/
│   ├── Citizens/
│   ├── ServiceRequests/
│   ├── Staff/
│   └── Reports/
├── wwwroot/             # Static files (CSS, JS, Images)
│   └── css/site.css
├── Migrations/          # Entity Framework migrations
├── Properties/          # Launch settings
├── appsettings.json     # Configuration settings
└── Program.cs           # Application entry point
```

## Prerequisites

- **.NET SDK**: Version 8.0 or higher
- **SQL Server**: Express or Higher Edition
- **Visual Studio** 2022 or **Visual Studio Code** with C# extension

## Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd MuncipalityManagementSystem
```

### 2. Configure Database Connection

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MuncipalityDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Replace `YOUR_SERVER` with your SQL Server instance name.

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

This creates the database and tables automatically.

### 4. Run the Application

```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Usage

### Accessing Features

- **Home Page**: Navigate to the home page for an overview
- **Citizens**: Manage citizen records (Create, Read, Update, Delete)
- **Service Requests**: Track and manage citizen service requests
- **Staff**: Manage municipality staff information
- **Reports**: Create and track reports
- **About & Contact**: Information pages

### Common Operations

#### Adding a New Citizen
1. Click "Citizens" in the navigation menu
2. Click "Add New Citizen" button
3. Fill in the required information (Name, Address, Phone, Email)
4. Click "Create"

#### Creating a Service Request
1. Navigate to "Service Requests"
2. Click "Create" button
3. Select a citizen and enter service details
4. Set the status and submit
5. Click "Create"

#### Managing Staff
1. Go to "Staff" section
2. View, add, edit, or delete staff members
3. Track departments and positions

## Database Models

### Citizen
- CitizenID (Primary Key)
- FullName (Required, Max 255 chars)
- Address (Required, Max 255 chars)
- PhoneNumber (Required, Max 20 chars)
- Email (Required, Email format)
- DateOfBirth (Optional)
- RegistrationDate (Auto-set)

### ServiceRequest
- RequestID (Primary Key)
- ServiceType (Required)
- RequestDate (Auto-set to current date)
- Status (Required)
- CitizenID (Foreign Key)

### Staff
- StaffID (Primary Key)
- FullName (Required)
- Position (Required)
- Department (Required)
- Email (Required)
- PhoneNumber (Required)
- HireDate

### Report
- ReportID (Primary Key)
- ReportType (Required)
- Details (Required)
- SubmissionDate (Auto-set)
- Status (Default: "Under Review")
- CitizenID (Foreign Key)

## Configuration

### appsettings.json

The main configuration file includes:
- **Logging**: Configure logging levels
- **AllowedHosts**: Specify allowed hosts (default: *)
- **ConnectionStrings**: Database connection configuration

### Launch Settings

Located in `Properties/launchSettings.json`, configure:
- Application URLs (HTTP/HTTPS)
- Environment variables
- Launch browser preferences

## Error Handling

The application includes comprehensive error handling:
- **Validation Errors**: Form validation with user-friendly messages
- **Database Errors**: Exception handling with meaningful error messages
- **Not Found Errors**: Proper 404 responses for missing resources
- **Success Messages**: User feedback for successful operations

## Security Features

- CSRF Protection via anti-forgery tokens
- Model validation on both client and server side
- SQL injection prevention via parameterized queries (EF Core)
- Email validation format

## Development

### Adding a New Feature

1. Create the model in the `Models` folder
2. Add DbSet to `ApplicationDbContext.cs`
3. Create a migration: `dotnet ef migrations add FeatureName`
4. Update database: `dotnet ef database update`
5. Generate controller scaffolding or create manually
6. Create corresponding Razor views

### Code Standards

- Use async/await for database operations
- Implement proper error handling
- Use meaningful variable and method names
- Add proper validation attributes to models
- Use TempData for user feedback messages

## Troubleshooting

### Database Connection Issues
- Verify SQL Server is running
- Check the connection string in appsettings.json
- Ensure `TrustServerCertificate=True` for self-signed certificates

### Migration Errors
```bash
# Reset migrations (be careful with production data!)
dotnet ef migrations remove
dotnet ef database update
```

### Port Already in Use
The application may be running on a different port. Check `Properties/launchSettings.json` for configured ports.

## Performance Considerations

- Database queries use async patterns for better scalability
- Views use table pagination for large datasets (recommended)
- Static files are served from wwwroot with caching enabled

## Future Enhancements

- Authentication and authorization (ASP.NET Identity)
- Advanced reporting and analytics
- Email notifications for service requests
- Pagination for large datasets
- Export functionality (CSV, PDF)
- Search and filtering capabilities
- API endpoints for mobile apps

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues, questions, or suggestions:
1. Check the troubleshooting section above
2. Review the code comments in key files
3. Check SQL Server error logs
4. Create an issue with detailed description

## Author

Created as a comprehensive municipality management system for .NET development.

---

**Last Updated**: February 2025