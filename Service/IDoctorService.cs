using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;

namespace HealthcareAppointmentApp.Service
{
    public interface IDoctorService
    {
        Task<IList<TimeSlot>> AddAvailabilityAsync(AvailabilityInsertDTO dto, long id);
        Task<IEnumerable<TimeSlot>> GetTimeslotsByUserId(long userId);
        Task<IEnumerable<TimeSlot>> GetFutureAvailableTimeslotsByDoctorId(long doctorId);
        Task<IEnumerable<Doctor>> GetBySpecialityAndCity(DoctorRequestDTO dto);
    }
}
