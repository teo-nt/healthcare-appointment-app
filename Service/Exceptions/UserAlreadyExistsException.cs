namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string s) : base(s) { }
    }
}
