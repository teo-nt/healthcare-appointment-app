using HealthcareAppointmentApp.Models;

namespace HealthcareAppointmentApp.DTO
{
    public class AppointmentReadOnlyDTO
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public DoctorDTO? Doctor { get; set; }
        public PatientDTO? Patient { get; set; }    
    }
}
