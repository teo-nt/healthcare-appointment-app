namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class DoctorNotFoundException : Exception
    {
        public DoctorNotFoundException(string s) : base(s) { }
    }
}
