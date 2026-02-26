using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MunicipalityManagementSystem.Models
{
	[Table("ServiceRequests")]
	public class ServiceRequest
	{
		[Key]
		public int RequestID { get; set; }

		[Required(ErrorMessage = "Service Type is required")]
		[MaxLength(255)]
		public string? ServiceType { get; set; }

		[Required(ErrorMessage = "Request Date is required")]
		public DateTime RequestDate { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "Status is required")]
		[MaxLength(50)]
		public string? Status { get; set; }

		[ForeignKey("Citizen")]
		[Required(ErrorMessage = "Citizen selection is required")]
		public int CitizenID { get; set; }

		public Citizen? Citizen { get; set; }
	}
}
