namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class TimeslotNotFoundException : Exception
    {
        public TimeslotNotFoundException(string s) : base(s) { }
    }
}
