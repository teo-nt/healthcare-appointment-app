using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HealthCareDbContext context) : base(context)
        {
        }

        public async Task<Doctor?> GetDoctorByPhoneNumber(string phoneNumber)
        {
            return await _context.Doctors.Where(d =>  d.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByCity(string city)
        {
            return await _context.Doctors.Where(d => d.City == city).ToListAsync();
        }

        /// <summary>
        /// Gets all active doctors by city and speciality.
        /// </summary>
        /// <param name="city">The city of doctor.</param>
        /// <param name="speciality">The speciality of doctor.</param>
        /// <returns>An <see cref="IEnumerable{Doctor}"/> that satisfy the criteria.</returns>
        public async Task<IEnumerable<Doctor>> GetDoctorsByCityAndSpeciality(string city, string speciality)
        {
            return await _context.Doctors
                .Where(d => d.User.Status == UserStatus.Approved &&
                        d.City == city && d.Speciality != null && 
                        d.Speciality.SpecialityName == speciality)
                .Include(d => d.Speciality)
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

        /// <summary>
        /// Gets all future available timeslots by doctor id.
        /// </summary>
        /// <param name="id">The id of doctor.</param>
        /// <returns>An <see cref="IEnumerable{TimeSlot}"/>.</returns>
        public async Task<IEnumerable<TimeSlot>> GetFutureAvailableTimeslotsByDoctorId(long id)
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);
            return await _context.TimeSlots.Where(t => t.DoctorId == id 
                                                && t.Status == AvailabilityStatus.Available
                                                && (t.Date > today || (t.Date == today && t.StartTimeSlot > currentTime)))
                .ToListAsync();
        }

        /// <summary>
        /// Gets all timeslots for a specific doctor.
        /// </summary>
        /// <param name="id">The id of doctor.</param>
        /// <returns>A list of timeslots.</returns>
        public async Task<IEnumerable<TimeSlot>> GetTimeSlots(long id)
        {
            return await _context.TimeSlots.Where(t => t.DoctorId == id).ToListAsync();
        }
    }
}
