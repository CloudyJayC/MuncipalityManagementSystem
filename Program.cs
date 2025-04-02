using Microsoft.EntityFrameworkCore;
using MuncipalityManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

//This adds Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//This adds MVC Services
builder.Services.AddControllersWithViews();

var app = builder.Build();

//This enables Static Files (CSS, JS, images)
app.UseStaticFiles();

//This enables Routing
app.UseRouting();

// This enables Authorization
app.UseAuthorization();

// Defines the Default Route (now points to Home instead of Citizens)
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
