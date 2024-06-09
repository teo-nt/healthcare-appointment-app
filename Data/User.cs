using HealthcareAppointmentApp.Models;

namespace HealthcareAppointmentApp.Data
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public UserStatus Status { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}
