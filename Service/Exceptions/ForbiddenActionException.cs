namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class ForbiddenActionException : Exception
    {
        public ForbiddenActionException(string s) : base(s) { }
    }
}
