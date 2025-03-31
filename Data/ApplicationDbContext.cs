//This file inherits from DbContext and contains the DbSet properties for the models. The ApplicationDbContext class is used to interact with the database using Entity Framework Core. The DbSet properties represent the tables in the database. The ApplicationDbContext class is registered in the Program.cs file using the AddDbContext method, which registers the DbContext with the DI container. This allows the DbContext to be injected into other classes that require database access.
using Microsoft.EntityFrameworkCore;
using MuncipalityManagementSystem.Models;

namespace MuncipalityManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
