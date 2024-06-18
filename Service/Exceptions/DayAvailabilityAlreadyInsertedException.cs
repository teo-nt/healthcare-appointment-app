namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class DayAvailabilityAlreadyInsertedException : Exception
    {
        public DayAvailabilityAlreadyInsertedException(string s) : base(s) { }
    }
}
