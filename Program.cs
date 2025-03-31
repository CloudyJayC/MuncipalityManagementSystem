using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MuncipalityManagementSystem.Data; // Namespace for ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// This adds the  Database Context (Registering DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// This adds MVC services (so Controllers and Views work)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// This enables MVC Routing (so it can find Controllers)
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
