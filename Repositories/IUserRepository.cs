using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string username, string password);
        Task<User?> GetByUsernameAsync(string username);
    }
}
