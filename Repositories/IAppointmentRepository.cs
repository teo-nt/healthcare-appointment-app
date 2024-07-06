using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetDoctorAppointments(long doctorId);
        Task<IEnumerable<Appointment>> GetPatientAppointments(long patientId);
    }
}
