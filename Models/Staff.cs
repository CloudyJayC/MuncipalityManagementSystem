using System;
using System.ComponentModel.DataAnnotations;

namespace MuncipalityManagementSystem.Models
{
	public class Staff
	{
		[Key]
		public int StaffID { get; set; }

		[Required]
		[StringLength(255)]
		public string FullName { get; set; }

		[Required]
		[StringLength(255)]
		public string Position { get; set; }

		[Required]
		[StringLength(255)]
		public string Department { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(255)]
		public string Email { get; set; }

		[Required]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

		public DateTime HireDate { get; set; }
	}
}
