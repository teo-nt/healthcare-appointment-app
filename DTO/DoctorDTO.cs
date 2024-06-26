using System.ComponentModel.DataAnnotations;

namespace HealthcareAppointmentApp.DTO
{
    public class DoctorDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Firstname should be at least 3 characters (max. 50)")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Lastname should be at least 3 characters (max. 50)")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "City should be 3-50 characters")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address should be 3-50 characters")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street Nr. is required")]
        [Range(1, uint.MaxValue, ErrorMessage = "Street number is not valid")]
        public uint StreetNumber { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Range(15, 60, MaximumIsExclusive = false, MinimumIsExclusive = false, ErrorMessage = "Appointment duration should be set " +
            "15-60 minutes (30 is the default)")]
        public uint AppointmentDuration { get; set; }

        public SpecialityReadOnlyDTO? Speciality { get; set; }
    }
}
