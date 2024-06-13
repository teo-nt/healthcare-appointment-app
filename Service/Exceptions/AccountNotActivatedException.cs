namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class AccountNotActivatedException : Exception
    {
        public AccountNotActivatedException(string s) : base(s) { }
    }
}
