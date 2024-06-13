namespace HealthcareAppointmentApp.DTO
{
    public class DoctorDTO
    {
        public long Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public uint StreetNumber { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public uint AppointmentDuration { get; set; }
        public SpecialityReadOnlyDTO? Speciality { get; set; }
    }
}
