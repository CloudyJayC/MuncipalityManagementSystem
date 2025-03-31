using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuncipalityManagementSystem.Models
{
	[Table("Citizens")] //Made sure to match with sql table name manually created with sql
	public class Citizen
	{
		[Key]
		public int CitizenID { get; set; }

		[Required, MaxLength(255)]
		public string FullName { get; set; }

		[Required, MaxLength(255)]
		public string Address { get; set; }

		[Required, MaxLength(20)]
		public string PhoneNumber { get; set; }

		[EmailAddress, MaxLength(255)]
		public string Email { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public DateTime RegistrationDate { get; set; }

		// Relationships
		public ICollection<ServiceRequest> ServiceRequests { get; set; }
		public ICollection<Report> Reports { get; set; }
	}
}
