using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatientsByLastname(string lastname);
        Task<Patient?> GetPatientByPhoneNumber(string phoneNumber);
    }
}
