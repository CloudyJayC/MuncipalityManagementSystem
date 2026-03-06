using System.ComponentModel.DataAnnotations;

namespace MunicipalityManagementSystem.Models
{
    public class AdminUserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? LinkedName { get; set; }
    }

    public class CreateStaffViewModel
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password),
         StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password),
         Compare("Password", ErrorMessage = "Passwords do not match."),
         Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, Display(Name = "Hire Date"),
         DataType(DataType.Date)]
        public DateTime HireDate { get; set; } = DateTime.Today;
    }
}
