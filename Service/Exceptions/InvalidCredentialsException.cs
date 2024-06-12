namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string s) : base(s) { }
    }
}
