using System.ComponentModel.DataAnnotations;

namespace HealthcareAppointmentApp.DTO
{
    public class PatientSignUpDTO : UserSignUpDTO
    {
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Firstname should be at least 3 characters (max. 50)")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Lastname should be at least 3 characters (max. 50)")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Range(18, 100, MinimumIsExclusive = false, MaximumIsExclusive = false, ErrorMessage = "Age should be 18-100")]
        public uint Age { get; set; }

        [RegularExpression(@"^[Ff]|[Mm]$")]
        public string? Gender { get; set; }

        [MinLength(20, ErrorMessage = "Medical history should have at least 20 characters.")]
        public string? MedicalHistory { get; set; }
    }
}
