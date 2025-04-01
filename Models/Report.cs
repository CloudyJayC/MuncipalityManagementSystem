using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuncipalityManagementSystem.Models
{
	[Table("Reports")] // Matches your SQL table name
	public class Report
	{
		[Key]
		public int ReportID { get; set; }

		[Display(Name = "Report Type")] // Add this to fix the label issue
		[Required, MaxLength(255)]
		public string ReportType { get; set; }

		[Required]
		public string Details { get; set; }

		public DateTime SubmissionDate { get; set; }

		[Required, MaxLength(50)]
		public string Status { get; set; }

		// Foreign Key
		[ForeignKey("Citizen")]
		[Display(Name = "Citizen")] // Ensures label shows "Citizen" instead of "CitizenID"
		public int CitizenID { get; set; }

		public Citizen Citizen { get; set; }
	}
}
