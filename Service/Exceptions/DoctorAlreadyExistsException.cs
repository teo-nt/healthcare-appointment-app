namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class DoctorAlreadyExistsException : Exception
    {
        public DoctorAlreadyExistsException(string s) : base(s) { }
    }
}
