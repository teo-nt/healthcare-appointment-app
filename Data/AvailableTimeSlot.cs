namespace HealthcareAppointmentApp.Data
{
    public class AvailableTimeSlot : BaseEntity
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTimeSlot { get; set; }
        public TimeOnly EndTimeSlot { get; set; }

        public long DoctorId {  get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
    }
}
