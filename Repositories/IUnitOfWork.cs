namespace HealthcareAppointmentApp.Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        DoctorRepository DoctorRepository { get; }
        PatientRepository PatientRepository { get; }
        SpecialityRepository SpecialityRepository { get; }
        AppointmentRepository AppointmentRepository { get; }
        TimeSlotRepository TimeSlotRepository { get; }

        Task<bool> SaveAsync();
    }
}
