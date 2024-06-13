namespace HealthcareAppointmentApp.DTO
{
    public class PatientDTO
    {
        public long Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public uint Age { get; set; }
        public string? Gender { get; set; }
        public string? MedicalHistory { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
