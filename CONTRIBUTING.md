# Contributing

Thanks for your interest in contributing! Here's how you can help.

## Reporting Bugs

Before opening a new issue, check if someone already reported it. When reporting bugs, include:

- Clear title (e.g., "Citizens page crashes when DateOfBirth is null")
- Steps to reproduce the problem
- What you expected to happen
- What actually happened
- Your environment (OS, .NET version, SQL Server version, browser)

**Example Bug Report:**
```
Title: ServiceRequests page throws NullReferenceException

Steps:
1. Go to Service Requests
2. Click any request
3. See error

Expected: Details page loads
Actual: NullReferenceException in ServiceRequestsController.cs line 45

Environment:
- Windows 11
- .NET 10.0.0
- SQL Server 2022
```

## Suggesting Features

Open an issue with:

- Clear description of the feature
- Why it would be useful
- Examples of how it would work
- Any mockups or screenshots if applicable

## Pull Requests

1. Fork the repo
2. Create a branch (`git checkout -b feature/your-feature`)
3. Make your changes
4. Test thoroughly
5. Commit (`git commit -m 'Add your feature'`)
6. Push to your fork (`git push origin feature/your-feature`)
7. Open a pull request

### Before Submitting

- [ ] Code follows the style guide below
- [ ] All tests pass
- [ ] Documentation updated if needed
- [ ] No sensitive data committed

## Development Setup

1. Fork and clone the repo
2. Install .NET SDK 10.0 and SQL Server
3. Update `appsettings.json` with your database connection
4. Run migrations: `dotnet ef database update`
5. Start the app: `dotnet run`

## Code Style

**Formatting:**
- 4 spaces for indentation (no tabs)
- Use tabs in `.editorconfig` format

**Naming:**
- PascalCase for classes, methods, properties
- camelCase for local variables and parameters
- Descriptive names (avoid abbreviations)

**Comments:**
- Add XML comments (`///`) for public classes and methods
- Comment complex logic
- Keep comments updated with code

**Example:**
```csharp
/// <summary>
/// Gets a citizen by ID.
/// </summary>
/// <param name="id">The citizen ID</param>
/// <returns>The citizen, or null if not found</returns>
public async Task<Citizen?> GetCitizenAsync(int id)
{
    if (id < 1)
        throw new ArgumentException("ID must be positive", nameof(id));

    try
    {
        return await _context.Citizens.FindAsync(id);
    }
    catch (DbUpdateException ex)
    {
        _logger.LogError(ex, "Error getting citizen {Id}", id);
        throw;
    }
}
```

**Async/Await:**
- Use `async`/`await` for database and I/O operations
- Suffix async methods with "Async"
- Don't use `.Result` or `.Wait()`

**Error Handling:**
- Catch specific exceptions
- Log errors appropriately
- Show user-friendly messages via TempData
- Don't swallow exceptions silently

## Commit Messages

- Use present tense: "Add feature" not "Added feature"
- Start with a verb: "Fix bug" not "Bug fix"
- Keep first line under 72 characters
- Reference issues: "Fixes #123"

**Good:**
```
Add email verification for citizens

- Send email on registration
- Add confirmation view
- Block login until verified

Fixes #456
```

**Bad:**
```
Fixed stuff
Updates
WIP
```

## Database Migrations

When changing the database schema:

1. Create migration: `dotnet ef migrations add DescriptiveName`
2. Review the generated code
3. Test: `dotnet ef database update`
4. Test rollback: `dotnet ef database update <previous>`
5. Commit migration files

**Good migration names:**
- AddEmailToStaff
- UpdateCitizenPhoneLength
- CreateReportsTable

**Bad migration names:**
- Update
- Changes
- Fix

## Testing

Before submitting, test:

- All CRUD operations work
- Forms validate correctly
- Error handling works
- Different browsers (if UI changes)
- Fresh database (clear cookies/cache)

## Questions?

- Check the README first
- Search existing issues
- Open a new issue with the `question` label

---

Thanks for contributing!
