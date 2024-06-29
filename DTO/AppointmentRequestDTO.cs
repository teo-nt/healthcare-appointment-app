namespace HealthcareAppointmentApp.DTO
{
    public class AppointmentRequestDTO
    {
        public long PatientUserId { get; set; }
        public long DoctorId { get; set; }
        public long TimeslotId { get; set; }
    }
}
