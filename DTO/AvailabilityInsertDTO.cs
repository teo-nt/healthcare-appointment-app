namespace HealthcareAppointmentApp.DTO
{
    public class AvailabilityInsertDTO
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
