using System;
using System.ComponentModel.DataAnnotations;

namespace MunicipalityManagementSystem.Models
{
    public class Citizen
    {
        [Key]
        public int CitizenID { get; set; }

        [Required, MaxLength(255)]
        public string? FullName { get; set; }

        [Required, MaxLength(100)]
        public string? StreetName { get; set; }

        [Required, MaxLength(100)]
        public string? Suburb { get; set; }

        [Required, MaxLength(100)]
        public string? City { get; set; }

        [Required, MaxLength(10)]
        public string? PostalCode { get; set; }

        [Required, MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        /// Returns the full address as: StreetName, Suburb, City, PostalCode
        public string Address => $"{StreetName}, {Suburb}, {City}, {PostalCode}";
    }
}
