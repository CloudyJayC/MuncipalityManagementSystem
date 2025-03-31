using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuncipalityManagementSystem.Models
{
	public class Citizen
	{
		[Key]
		public int CitizenID { get; set; }

		[Required]
		[StringLength(255)]
		public string FullName { get; set; }

		[Required]
		[StringLength(255)]
		public string Address { get; set; }

		[Required]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

		[EmailAddress]
		[StringLength(255)]
		public string Email { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public DateTime RegistrationDate { get; set; } = DateTime.Now;

		public ICollection<ServiceRequest> ServiceRequests { get; set; }
		public ICollection<Report> Reports { get; set; }
	}
}
