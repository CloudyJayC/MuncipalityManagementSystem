using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MunicipalityManagementSystem.Models
{
	[Table("Staffs")]
	public class Staff
	{
		[Key]
		public int StaffID { get; set; }

		[Required, MaxLength(255)]
		public string FullName { get; set; } = string.Empty;

		[Required, MaxLength(255)]
		public string Position { get; set; } = string.Empty;

		[Required, MaxLength(255)]
		public string Department { get; set; } = string.Empty;

		[Required, EmailAddress, MaxLength(255)]
		public string Email { get; set; } = string.Empty;

		[Required, MaxLength(20)]
		public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public string? UserId { get; set; }
    }
}
