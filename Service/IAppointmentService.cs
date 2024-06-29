using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;

namespace HealthcareAppointmentApp.Service
{
    public interface IAppointmentService
    {
        Task<Appointment> BookAppointment(AppointmentRequestDTO dto);
    }
}
