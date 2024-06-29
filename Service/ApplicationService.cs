using AutoMapper;
using HealthcareAppointmentApp.Repositories;

namespace HealthcareAppointmentApp.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _userLogger;
        private readonly ILogger<DoctorService> _doctorLogger;
        private readonly ILogger<AppointmentService> _appointmentLogger;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger<UserService> userLogger, 
            ILogger<DoctorService> doctorLogger, ILogger<AppointmentService> appointmentLogger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userLogger = userLogger;
            _doctorLogger = doctorLogger;
            _appointmentLogger = appointmentLogger;
            _mapper = mapper;
        }

        public IUserService UserService => new UserService(_unitOfWork, _userLogger, _mapper);
        public IDoctorService DoctorService => new DoctorService(_unitOfWork, _doctorLogger);
        public IAppointmentService AppointmentService => new AppointmentService(_unitOfWork, _appointmentLogger);
    }
}
