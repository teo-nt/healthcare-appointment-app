using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.Models;

namespace HealthcareAppointmentApp.DTO
{
    public class UserDetailsDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public UserStatus Status { get; set; }

        public PatientDTO? Patient { get; set; }
        public DoctorDTO? Doctor { get; set; }
    }
}
