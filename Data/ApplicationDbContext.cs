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

			// Define relationships
			modelBuilder.Entity<ServiceRequest>()
				.HasOne(s => s.Citizen)
				.WithMany(c => c.ServiceRequests)
				.HasForeignKey(s => s.CitizenID);

			modelBuilder.Entity<Report>()
				.HasOne(r => r.Citizen)
				.WithMany(c => c.Reports)
				.HasForeignKey(r => r.CitizenID);
		}
	}
}
