using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuncipalityManagementSystem.Models
{
	[Table("ServiceRequests")] // Matches your SQL table name
	public class ServiceRequest
	{
		[Key]
		public int RequestID { get; set; }

		[Required, MaxLength(255)]
		public string ServiceType { get; set; }

		public DateTime RequestDate { get; set; }

		[Required, MaxLength(50)]
		public string Status { get; set; }

		// Foreign Key
		[ForeignKey("Citizen")]
		public int CitizenID { get; set; }
		public Citizen Citizen { get; set; }
	}
}
