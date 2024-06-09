using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthCareDbContext _context;

        public UnitOfWork(HealthCareDbContext context)
        {
            _context = context;
        }

        public UserRepository UserRepository => new UserRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
