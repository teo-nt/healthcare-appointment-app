using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>
    {
        public AppointmentRepository(HealthCareDbContext context) : base(context)
        {
        }
    }
}
