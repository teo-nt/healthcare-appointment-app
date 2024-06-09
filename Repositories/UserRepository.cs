using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.Security;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HealthCareDbContext context) : base(context) { }

        /// <summary>
        /// Returns the user based on username.
        /// </summary>
        /// <param name="username">The username of user.</param>
        /// <returns>The user if the username exists, null otherwise.</returns>
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the user based on username and password.
        /// </summary>
        /// <param name="username">The username of user.</param>
        /// <param name="password">The password of user.</param>
        /// <returns>The user if username and password are valid, null otherwise.</returns>
        public async Task<User?> GetUserAsync(string username, string password)
        {
            User? user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            if (user is null) return null;
            if (!EncryptionUtil.IsValidPassword(password, user.Password)) return null;
            return user;
        }
    }
}
