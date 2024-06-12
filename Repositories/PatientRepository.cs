using HealthcareAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HealthCareDbContext context) : base(context) { }

        public async Task<Patient?> GetPatientByPhoneNumber(string phoneNumber)
        {
            return await _context.Patients.Where(p => p.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Patient>> GetPatientsByLastname(string lastname)
        {
            return await _context.Patients.Where(p => p.Lastname.Contains(lastname)).ToListAsync();
        }
    }
}
