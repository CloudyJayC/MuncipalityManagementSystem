using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Hubs;
using MunicipalityManagementSystem.Models;
using MunicipalityManagementSystem.Services;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
if (builder.Environment.IsDevelopment())
{
	builder.Logging.SetMinimumLevel(LogLevel.Debug);
}
else
{
	builder.Logging.SetMinimumLevel(LogLevel.Information);
}

// Configure database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = false;
	options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure authentication cookie paths for Identity
builder.Services.ConfigureApplicationCookie(options => {
	options.LoginPath = "/Identity/Account/Login";
	options.LogoutPath = "/Identity/Account/Logout";
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Register email sender service
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Register notification service
builder.Services.AddScoped<NotificationService>();

// Register SignalR for real-time notifications
builder.Services.AddSignalR();

// Configure MVC services with antiforgery
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery();

var app = builder.Build();

// Configure exception handling middleware
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
else
{
	// Development-specific middleware
	app.UseDeveloperExceptionPage();
}

// Global error logging middleware
app.Use(async (context, next) =>
{
	try
	{
		await next();
	}
	catch (Exception ex)
	{
		var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "Unhandled exception occurred during request processing");
		
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		context.Response.ContentType = "text/plain";
		await context.Response.WriteAsync("An error occurred. Please try again later.");
	}
});

// Security headers middleware
app.Use(async (context, next) =>
{
	context.Response.Headers["X-Content-Type-Options"] = "nosniff";
	context.Response.Headers["X-Frame-Options"] = "DENY";
	context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
	context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
	await next();
});

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable static files for CSS, JS, images
app.UseStaticFiles();

// Configure routing
app.UseRouting();

// Configure authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Map SignalR hub endpoint
app.MapHub<NotificationHub>("/notificationHub");

// ── Seed roles and accounts ───────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    // Ensure all roles exist in every environment
    string[] roles = { "Admin", "Staff", "Citizen" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // ── Seed admin account (all environments) ─────────────────────────────
    string adminEmail = config["Seed:AdminEmail"]
        ?? throw new InvalidOperationException("Seed:AdminEmail is not configured.");
    string adminPassword = config["Seed:AdminPassword"]
        ?? throw new InvalidOperationException("Seed:AdminPassword is not configured.");

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "System",
            LastName = "Admin",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            logger.LogInformation("Seeded admin account: {Email}", adminEmail);
        }
        else
        {
            logger.LogError("Failed to seed admin account: {Errors}",
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    // ── Seed test staff account (development only) ─────────────────────────
    if (app.Environment.IsDevelopment())
    {
        string staffEmail = config["Seed:StaffEmail"]
            ?? throw new InvalidOperationException("Seed:StaffEmail is not configured.");
        string staffPassword = config["Seed:StaffPassword"]
            ?? throw new InvalidOperationException("Seed:StaffPassword is not configured.");

        if (await userManager.FindByEmailAsync(staffEmail) == null)
        {
            var staffUser = new ApplicationUser
            {
                UserName = staffEmail,
                Email = staffEmail,
                FirstName = "Test",
                LastName = "Staff",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(staffUser, staffPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(staffUser, "Staff");

                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Staffs.Add(new MunicipalityManagementSystem.Models.Staff
                {
                    FullName = "Test Staff",
                    Position = "General Staff",
                    Department = "Administration",
                    Email = staffEmail,
                    PhoneNumber = "+27000000000",
                    HireDate = DateTime.Now,
                    UserId = staffUser.Id
                });
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Seeded development staff account: {Email}", staffEmail);
            }
        }
    }
}

app.Run();
