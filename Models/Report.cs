using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuncipalityManagementSystem.Models
{
	[Table("Reports")]
	public class Report
	{
		[Key]
		public int ReportID { get; set; }

		[Display(Name = "Report Type")]
		[Required(ErrorMessage = "Report type is required")]
		[MaxLength(255)]
		public string? ReportType { get; set; }

		[Required(ErrorMessage = "Details are required")]
		public string? Details { get; set; }

		[Display(Name = "Submission Date")]
		[DataType(DataType.Date)]
		public DateTime SubmissionDate { get; set; } = DateTime.Now;

		[Display(Name = "Status")]
		[Required(ErrorMessage = "Status is required")]
		[MaxLength(50)]
		public string? Status { get; set; } = "Under Review";

		// Foreign Key
		[ForeignKey("Citizen")]
		[Display(Name = "Citizen")]
		[Required(ErrorMessage = "Please select a citizen")]
		public int CitizenID { get; set; }

		public Citizen? Citizen { get; set; }
	}
}