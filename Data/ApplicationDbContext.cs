using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MunicipalityManagementSystem.Models;

namespace MunicipalityManagementSystem.Data
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

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.ConfigureWarnings(w => 
				w.Ignore(RelationalEventId.PendingModelChangesWarning));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Citizen>().ToTable("Citizens");
			modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequests");
			modelBuilder.Entity<Staff>().ToTable("Staffs");
			modelBuilder.Entity<Report>().ToTable("Reports");
		}
	}
}
