using HealthcareAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HealthCareDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByCity(string city)
        {
            return await _context.Doctors.Where(d => d.City == city).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByCityAndSpeciality(string city, string speciality)
        {
            return await _context.Doctors
                .Where(d => d.City == city && d.Speciality != null && d.Speciality.SpecialityName == speciality)
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByLastname(string lastname)
        {
            return await _context.Doctors.Where(d => d.Lastname.Contains(lastname)).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpeciality(string speciality)
        {
            return await _context.Doctors
                .Where(d => d.Speciality != null 
                        && d.Speciality.SpecialityName.Contains(speciality))
                .ToListAsync();
        }
    }
}
