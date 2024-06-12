using HealthcareAppointmentApp.Data;

namespace HealthcareAppointmentApp.Repositories
{
    public class AvailableTimeSlotRepository : BaseRepository<AvailableTimeSlot>
    {
        public AvailableTimeSlotRepository(HealthCareDbContext context) : base(context)
        {
        }
    }
}
