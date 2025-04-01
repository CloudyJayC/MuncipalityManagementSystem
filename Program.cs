using Microsoft.EntityFrameworkCore;
using MuncipalityManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Enable Static Files (CSS, JS, images)
app.UseStaticFiles();

// Enable Routing
app.UseRouting();

// Enable Authorization
app.UseAuthorization();

// Define the Default Route
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Citizens}/{action=Index}/{id?}");

app.Run();
