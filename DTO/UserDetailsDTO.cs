using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthcareAppointmentApp.DTO
{
    public class UserDetailsDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username should be 5 - 30 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = string.Empty;

        public UserRole UserRole { get; set; }
        public UserStatus Status { get; set; }

        public PatientDTO? Patient { get; set; }
        public DoctorDTO? Doctor { get; set; }
    }
}
