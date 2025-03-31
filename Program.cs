using Microsoft.EntityFrameworkCore;
using MuncipalityManagementSystem.Data; // Namespace for ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// This adds the Database Context (Registering DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// This adds MVC services (so Controllers and Views work)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Enable Static Files (CSS, JS, images)
app.UseStaticFiles();

// Enable Routing
app.UseRouting();

// Enable Authorization (for future authentication)
app.UseAuthorization();

// Define the Default Route
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
