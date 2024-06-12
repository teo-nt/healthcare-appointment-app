using HealthcareAppointmentApp.Models;

namespace HealthcareAppointmentApp.DTO
{
    public class UserReadOnlyDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;     
        public UserRole UserRole { get; set; }
        public UserStatus Status { get; set; }
    }
}
