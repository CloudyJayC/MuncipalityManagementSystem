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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//This ensured the entity framework knows that we are using an existing database and it doesnt create one
			modelBuilder.Entity<Citizen>().ToTable("Citizens");
			modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequests");
			modelBuilder.Entity<Staff>().ToTable("Staff");
			modelBuilder.Entity<Report>().ToTable("Reports");
		}
	}
}
