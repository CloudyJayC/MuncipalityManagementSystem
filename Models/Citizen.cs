using System;
using System.ComponentModel.DataAnnotations;

namespace MuncipalityManagementSystem.Models
{
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

		[Required, EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DateOfBirth { get; set; }

		[DataType(DataType.Date)]
		public DateTime RegistrationDate { get; set; } = DateTime.Now; // Automatically set default value
	}
}
