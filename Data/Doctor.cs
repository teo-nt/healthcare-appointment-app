namespace HealthcareAppointmentApp.Data
{
    public class Doctor : BaseEntity
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public uint StreetNumber { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public uint AppointmentDuration { get; set; }

        public long? SpecialityId { get; set; }
        public virtual Speciality? Speciality { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<AvailableTimeSlot> AvailableTimeSlots { get; } = new HashSet<AvailableTimeSlot>();
        public virtual ICollection<Appointment> Appointments { get; } = new HashSet<Appointment>();
    }
}
