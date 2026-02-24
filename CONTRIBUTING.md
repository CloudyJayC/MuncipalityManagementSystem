# Contributing to Municipality Management System

First off, thank you for considering contributing to the Municipality Management System! It's people like you that make this system such a great tool.

## Code of Conduct

This project and everyone participating in it is governed by our Code of Conduct. By participating, you are expected to uphold this code:

- Use welcoming and inclusive language
- Be respectful of differing opinions and experiences
- Accept constructive criticism gracefully
- Focus on what is best for the community
- Show empathy towards other community members

## How Can I Contribute?

### Reporting Bugs

Before reporting a bug, please check the [issue list](../../issues) as you might find out that you don't need to create one. When you are creating a bug report, please include as many details as possible:

* **Use a clear and descriptive title** (e.g., "Citizens page crashes when DateOfBirth is null")
* **Describe the exact steps which reproduce the problem** (with numbered steps)
* **Provide specific examples to demonstrate the steps**
* **Describe the behavior you observed after following the steps**
* **Explain which behavior you expected to see instead and why**
* **Include screenshots and animated GIFs if possible**
* **Include your environment details**:
  - Operating System (Windows, Linux, macOS)
  - .NET version (`dotnet --version`)
  - SQL Server version
  - Browser (if web-related)

**Bug Report Example**:
```
Title: ServiceRequests page throws NullReferenceException

Steps to reproduce:
1. Navigate to Service Requests page
2. Click on any service request
3. Observe error

Expected: Details page should load
Actual: NullReferenceException in ServiceRequestsController.cs line 45

Environment:
- Windows 11
- .NET 10.0.0
- SQL Server 2022
```

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion, please include:

* **Use a clear and descriptive title** (e.g., "Add email notifications for new service requests")
* **Provide a step-by-step description of the suggested enhancement**
* **Provide specific examples to demonstrate the use case**
* **Describe the current behavior and the expected behavior**
* **Explain why this enhancement would be useful**
* **List any additional context** (screenshots, mockups, etc.)

### Pull Requests

* Fill in the pull request template provided
* Follow the C# styleguides (see below)
* Ensure all files end with a newline
* Add meaningful commit messages
* Test your changes thoroughly
* Update documentation and README if needed
* Keep pull requests focused on a single feature or fix
* Link to any related issues

**PR Checklist**:
- [ ] My code follows the code style guidelines
- [ ] I have updated documentation (README, comments, etc.)
- [ ] I have added proper error handling
- [ ] I have tested my changes thoroughly
- [ ] My commit messages are clear and descriptive
- [ ] I have not committed sensitive information

## Development Setup

### Prerequisites

- .NET SDK 10.0 or later
- SQL Server (Express or full edition)
- Visual Studio, VS Code, or JetBrains Rider
- Git

### Local Development Environment

1. **Fork the repository**
   
   Click the "Fork" button at the top of this repository.

2. **Clone your fork locally**

   ```bash
   git clone https://github.com/YOUR_USERNAME/MunicipalityManagementSystem.git
   cd MunicipalityManagementSystem
   ```

3. **Add upstream remote**

   ```bash
   git remote add upstream https://github.com/ORIGINAL_OWNER/MunicipalityManagementSystem.git
   ```

4. **Create a feature branch**

   ```bash
   git checkout -b feature/AmazingFeature
   ```

5. **Configure database connection**

   Edit `appsettings.Development.json` and update the connection string:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER\\SQLEXPRESS;Database=MicipalityDB_Dev;Trusted_Connection=True;..."
     },
     "Logging": {
       "LogLevel": {
         "Default": "Debug"
       }
     }
   }
   ```

6. **Apply migrations**

   ```bash
   dotnet ef database update
   ```

7. **Run the application**

   ```bash
   dotnet run
   ```

8. **Make your changes and test**

9. **Commit your changes**

   ```bash
   git commit -m 'Add AmazingFeature'
   ```

10. **Push to your fork**

    ```bash
    git push origin feature/AmazingFeature
    ```

11. **Open a Pull Request**

    Go to the original repository and click "Compare & pull request"

## Styleguides

### C# Code Style

**Indentation**:
- Use 4 spaces for indentation (no tabs)
- Ensure consistent indentation throughout files

**Naming Conventions**:
- Use PascalCase for class names, method names, and properties
- Use camelCase for local variables and parameters
- Use CONSTANT_CASE for constants
- Use meaningful, descriptive names
- Avoid abbreviations except for well-known terms

**Comments and Documentation**:
- Add XML documentation comments for public classes, methods, and properties
- Use `///` for XML comments
- Add meaningful inline comments for complex logic
- Keep comments up-to-date with code changes

**Example**:
```csharp
/// <summary>
/// Retrieves a citizen by their ID from the database.
/// </summary>
/// <param name="id">The unique identifier of the citizen</param>
/// <returns>The citizen if found; otherwise null</returns>
/// <exception cref="ArgumentException">Thrown when id is less than 1</exception>
public async Task<Citizen?> GetCitizenByIdAsync(int id)
{
    if (id < 1)
    {
        throw new ArgumentException("Citizen ID must be greater than 0", nameof(id));
    }

    try
    {
        return await _context.Citizens.FindAsync(id);
    }
    catch (DbUpdateException ex)
    {
        _logger.LogError(ex, "Database error retrieving citizen with ID {CitizenId}", id);
        throw;
    }
}
```

**Async/Await**:
- Use `async`/`await` for all I/O operations (database, HTTP, file system)
- Always use `Task` or `Task<T>` for async methods
- Suffix async methods with "Async" (e.g., `GetCitizenAsync`)
- Avoid blocking calls like `.Result` or `.Wait()`

**Exception Handling**:
- Use try-catch blocks for operations that can fail
- Log exceptions with appropriate log levels
- Provide meaningful error messages to users via TempData
- Never silently swallow exceptions

**Null-Safety**:
- Enable nullable reference types (already enabled in this project)
- Use `string?` for nullable strings
- Handle null cases explicitly

### Git Commit Messages

- **Use the present tense**: "Add feature" not "Added feature"
- **Use the imperative mood**: "Move cursor to..." not "Moves cursor to..."
- **Limit the first line to 72 characters**
- **Capitalize the first letter**
- **Reference issues and pull requests** after the first line: "Fixes #123"
- **Explain what and why**, not how

**Good Commit Messages**:
```
Add email verification for citizen accounts

- Send verification email on registration
- Add email confirmation view
- Prevent login until email is verified

Fixes #456
```

**Bad Commit Messages**:
```
Fixed stuff
WIP
Made changes
```

### Razor View Style

- Use meaningful variable names in views
- Keep logic minimal in views (use ViewBag/ViewData or ViewModel)
- Use Bootstrap classes for styling
- Include proper form validation attributes
- Add ARIA labels for accessibility

### Database Migrations

When adding or modifying database schema:

1. **Create a descriptive migration name**

   ```bash
   dotnet ef migrations add AddEmailToStaff
   ```

   **Good names**: `AddEmailColumnToStaff`, `UpdateCitizenPhoneLength`  
   **Bad names**: `Update`, `Change`, `Fix`

2. **Review the generated migration file**

3. **Test the migration**:

   ```bash
   dotnet ef database update
   dotnet ef database update <previous_migration> # Rollback test
   dotnet ef database update AddEmailToStaff      # Re-apply
   ```

4. **Commit the migration files**

### Documentation

- Update README.md if you change functionality
- Add XML documentation comments for public APIs
- Include examples in comments for complex features
- Update this CONTRIBUTING.md if you change development setup
- Document any new dependencies or external services

## Testing

Before submitting a pull request:

1. **Test locally** with `dotnet run`
2. **Test all CRUD operations** on affected entities
3. **Test error handling** (null values, invalid IDs, etc.)
4. **Test validation** on forms
5. **Clear your cookies/cache** to verify fresh session behavior
6. **Test in different browsers** if UI changes

## Issue and Pull Request Labels

Repository maintainers use labels to organize and prioritize work:

- `bug` - Something isn't working
- `enhancement` - New feature or request
- `documentation` - Improvements or additions to documentation
- `good first issue` - Good for newcomers to the project
- `help wanted` - Extra attention is needed
- `question` - Further information is requested
- `wontfix` - This will not be worked on
- `in progress` - Currently being worked on

## Review Process

1. **Automated checks** run on all pull requests
2. **Code review** by maintainers (can take 3-7 days)
3. **Requested changes** must be addressed
4. **Approval** from at least one maintainer
5. **Merge** into main branch

## Recognition

Contributors will be:
- Listed in the project's contributor section
- Credited in commit history
- Acknowledged in release notes (for significant contributions)

## Questions?

- Check the [README](README.md) for project documentation
- Search [existing issues](../../issues) for answers
- Open a [new issue](../../issues/new) with the `question` label
- Contact maintainers in pull request discussions

---

Thank you for your contributions and for helping make Municipality Management System better!
