using HealthcareAppointmentApp.Models;

namespace HealthcareAppointmentApp.Data
{
    public class Appointment : BaseEntity
    {
        public DateTime DateTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }

        public long? PatientId { get; set; }
        public virtual Patient? Patient { get; set; } = null!;

        public long DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
    }
}
