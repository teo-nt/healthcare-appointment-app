using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public interface ISpecialityRepository
    {
        Task<Speciality?> GetSpecialityByNameAsync(string specialityName);
    }
}
