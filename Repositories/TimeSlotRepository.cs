using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public class TimeSlotRepository : BaseRepository<TimeSlot>
    {
        public TimeSlotRepository(HealthCareDbContext context) : base(context)
        {
        }
    }
}
