using HealthcareAppointmentApp.Repositories;

namespace HealthcareAppointmentApp.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _userLogger;
        private readonly ILogger<DoctorService> _doctorLogger;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger<UserService> userLogger, ILogger<DoctorService> doctorLogger)
        {
            _unitOfWork = unitOfWork;
            _userLogger = userLogger;
            _doctorLogger = doctorLogger;
        }

        public IUserService UserService => new UserService(_unitOfWork, _userLogger);
        public IDoctorService DoctorService => new DoctorService(_unitOfWork, _doctorLogger);
    }
}
