using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuncipalityManagementSystem.Models
{
	[Table("Staff")] // Matches your SQL table name
	public class Staff
	{
		[Key]
		public int StaffID { get; set; }

		[Required, MaxLength(255)]
		public string FullName { get; set; }

		[Required, MaxLength(255)]
		public string Position { get; set; }

		[Required, MaxLength(255)]
		public string Department { get; set; }

		[Required, EmailAddress, MaxLength(255)]
		public string Email { get; set; }

		[Required, MaxLength(20)]
		public string PhoneNumber { get; set; }

		public DateTime HireDate { get; set; }
	}
}
