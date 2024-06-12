namespace HealthcareAppointmentApp.Service.Exceptions
{
    public class PatientAlreadyExistsException : Exception
    {
        public PatientAlreadyExistsException(string s) : base(s) { }
    }
}
