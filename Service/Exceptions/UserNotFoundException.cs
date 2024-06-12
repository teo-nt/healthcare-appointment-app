namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string s) : base(s) { }
    }
}
