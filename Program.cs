using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;

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

// Configure MVC services with antiforgery
builder.Services.AddControllersWithViews();
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
app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
