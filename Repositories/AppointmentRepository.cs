using HealthcareAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HealthCareDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets doctor appointments with patient details.
        /// </summary>
        /// <param name="doctorId">The doctor id.</param>
        /// <returns>Appointments for that doctor.</returns>
        public async Task<IEnumerable<Appointment>> GetDoctorAppointments(long doctorId)
        {
            return await _context.Appointments.Where(a => a.DoctorId == doctorId)
                        .Include(a => a.Patient)
                        .OrderBy(a => a.DateTime)
                        .ToListAsync();
        }

        /// <summary>
        /// Gets patient appointments with doctor details.
        /// </summary>
        /// <param name="patientId">The patient id.</param>
        /// <returns>Appointments for that patient.</returns>
        public async Task<IEnumerable<Appointment>> GetPatientAppointments(long patientId)
        {
            return await _context.Appointments.Where(a => a.PatientId == patientId)
                        .Include(a => a.Doctor)
                        .ThenInclude(d => d.Speciality)
                        .OrderBy(a => a.DateTime)
                        .ToListAsync();
        }
    }
}
