using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuncipalityManagementSystem.Models
{
	public class ServiceRequest
	{
		[Key]
		public int RequestID { get; set; }

		[ForeignKey("Citizen")]
		public int CitizenID { get; set; }

		[Required]
		[StringLength(255)]
		public string ServiceType { get; set; }

		public DateTime RequestDate { get; set; } = DateTime.Now;

		[Required]
		[StringLength(50)]
		public string Status { get; set; } = "Pending";

		public Citizen Citizen { get; set; }
	}
}
