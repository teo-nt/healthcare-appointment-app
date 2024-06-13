using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
        Task<User?> GetUserAsync(string username, string password);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByUsernameOrEmailAsync(string username, string email);
        Task<User?> GetByEmailAsync(string email);
    }
}
