namespace HealthcareAppointmentApp.Data
{
    public class Patient : BaseEntity
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public uint Age { get; set; }
        public string? Gender { get; set; }
        public string? MedicalHistory { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; } = new HashSet<Appointment>();
    }
}
