namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class AccountAlreadyActivatedException : Exception
    {
        public AccountAlreadyActivatedException(string s) : base(s) { }
    }
}
