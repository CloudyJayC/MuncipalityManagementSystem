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

		[Required, MaxLength(255)]
		public string ReportType { get; set; }

		[Required]
		public string Details { get; set; }

		public DateTime SubmissionDate { get; set; }

		[Required, MaxLength(50)]
		public string Status { get; set; }

		// Foreign Key
		[ForeignKey("Citizen")]
		public int CitizenID { get; set; }
		public Citizen Citizen { get; set; }
	}
}
