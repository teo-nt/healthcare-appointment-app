using HealthcareAppointmentApp.Models;

namespace HealthcareAppointmentApp.DTO
{
    public class AvailableTimeslotDTO
    {
        public long Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTimeSlot { get; set; }
        public TimeOnly EndTimeSlot { get; set; }
        public AvailabilityStatus Status { get; set; }
    }
}
