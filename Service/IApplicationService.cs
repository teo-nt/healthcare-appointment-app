namespace HealthcareAppointmentApp.Service
{
    public interface IApplicationService
    {
        IUserService UserService { get; }
        IDoctorService DoctorService { get; }
    }
}
