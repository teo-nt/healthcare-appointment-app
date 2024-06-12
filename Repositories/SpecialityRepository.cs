using HealthcareAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Repositories
{
    public class SpecialityRepository : BaseRepository<Speciality>, ISpecialityRepository
    {
        public SpecialityRepository(HealthCareDbContext context) : base(context)
        {
        }

        public async Task<Speciality?> GetSpecialityByNameAsync(string specialityName)
        {
            return await _context.Specialities.Where(s => s.SpecialityName == specialityName).FirstOrDefaultAsync();
        }
    }
}
