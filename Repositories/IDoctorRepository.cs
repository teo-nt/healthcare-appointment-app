using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctorsByLastname(string lastname);
        Task<IEnumerable<Doctor>> GetDoctorsBySpeciality(string speciality);
        Task<IEnumerable<Doctor>> GetDoctorsByCity(string city);
        Task<IEnumerable<Doctor>> GetDoctorsByCityAndSpeciality(string city, string speciality);
        Task<Doctor?> GetDoctorByPhoneNumber(string phoneNumber);
    }
}
