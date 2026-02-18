# Contributing to Municipality Management System

First off, thank you for considering contributing to the Municipality Management System! It's people like you that make this system such a great tool.

## Code of Conduct

This project and everyone participating in it is governed by our Code of Conduct. By participating, you are expected to uphold this code.

## How Can I Contribute?

### Reporting Bugs

Before reporting a bug, please check the issue list as you might find out that you don't need to create one. When you are creating a bug report, please include as many details as possible:

* **Use a clear and descriptive title**
* **Describe the exact steps which reproduce the problem**
* **Provide specific examples to demonstrate the steps**
* **Describe the behavior you observed after following the steps**
* **Explain which behavior you expected to see instead and why**
* **Include screenshots and animated GIFs if possible**
* **Include your environment details** (OS, .NET version, SQL Server version, etc.)

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion, please include:

* **Use a clear and descriptive title**
* **Provide a step-by-step description of the suggested enhancement**
* **Provide specific examples to demonstrate the steps**
* **Describe the current behavior and the expected behavior**
* **Explain why this enhancement would be useful**

### Pull Requests

* Fill in the required template
* Follow the C# styleguides
* End all files with a newline
* Add appropriate tests for your changes

## Development Setup

1. Fork the repository
2. Clone your fork locally
3. Create a new branch for your feature (`git checkout -b feature/AmazingFeature`)
4. Configure your local database connection in `appsettings.json`
5. Apply migrations: `dotnet ef database update`
6. Make your changes
7. Test thoroughly
8. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
9. Push to the branch (`git push origin feature/AmazingFeature`)
10. Open a Pull Request

## Styleguides

### C# Code Style

* Use 4 spaces for indentation
* Follow Microsoft C# Coding Conventions
* Use meaningful variable and method names
* Add XML documentation comments for public methods and classes
* Use async/await for asynchronous operations
* Use proper exception handling

Example:
```csharp
/// <summary>
/// Gets a citizen by their ID.
/// </summary>
/// <param name="id">The citizen ID</param>
/// <returns>The citizen if found; otherwise null</returns>
public async Task<Citizen> GetCitizenAsync(int id)
{
    try
    {
        return await _context.Citizens.FindAsync(id);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error retrieving citizen with ID {CitizenId}", id);
        throw;
    }
}
```

### Git Commit Messages

* Use the present tense ("Add feature" not "Added feature")
* Use the imperative mood ("Move cursor to..." not "Moves cursor to...")
* Limit the first line to 72 characters or less
* Reference issues and pull requests liberally after the first line

### Documentation

* Use clear and descriptive language
* Include code examples where appropriate
* Update the README if you change functionality
* Add comments for complex logic

## Additional Notes

### Issue and Pull Request Labels

This section lists the labels we use to help organize and categorize issues and pull requests.

* `bug` - Something isn't working
* `enhancement` - New feature or request
* `documentation` - Improvements or additions to documentation
* `good first issue` - Good for newcomers
* `help wanted` - Extra attention is needed
* `question` - Further information is requested
* `wontfix` - This will not be worked on

## Recognition

Contributors will be recognized in the project's contributor list and commit history.

## Questions?

Feel free to open an issue with the question tag, and we'll get back to you as soon as we can.

Thank you for contributing!
