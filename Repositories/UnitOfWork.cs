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
        public DoctorRepository DoctorRepository => new DoctorRepository(_context);
        public SpecialityRepository SpecialityRepository => new SpecialityRepository(_context);
        public PatientRepository PatientRepository => new PatientRepository(_context);
        public AppointmentRepository AppointmentRepository => new AppointmentRepository(_context);
        public TimeSlotRepository TimeSlotRepository => new TimeSlotRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
