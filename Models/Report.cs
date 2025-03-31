using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//All the necessary namespaces are included and properties asked in instructions mentioned
namespace MuncipalityManagementSystem.Models
{
	public class Report
	{
		[Key]
		public int ReportID { get; set; }

		[ForeignKey("Citizen")]
		public int CitizenID { get; set; }

		[Required]
		[StringLength(255)]
		public string ReportType { get; set; }

		[Required]
		public string Details { get; set; }

		public DateTime SubmissionDate { get; set; } = DateTime.Now;

		[Required]
		[StringLength(50)]
		public string Status { get; set; } = "Under Review";

		public Citizen Citizen { get; set; }
	}
}
