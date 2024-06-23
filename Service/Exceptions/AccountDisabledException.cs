namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class AccountDisabledException : Exception
    {
        public AccountDisabledException(string s) : base(s) { }
    }
}
