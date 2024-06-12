namespace HealthcareAppointmentApp.Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        DoctorRepository DoctorRepository { get; }
        PatientRepository PatientRepository { get; }
        SpecialityRepository SpecialityRepository { get; }
        AppointmentRepository AppointmentRepository { get; }
        AvailableTimeSlotRepository AvailableTimeSlotRepository { get; }

        Task<bool> SaveAsync();
    }
}
