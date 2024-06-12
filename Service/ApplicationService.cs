using HealthcareAppointmentApp.Repositories;

namespace HealthcareAppointmentApp.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IUserService UserService => new UserService(_unitOfWork, _logger);
    }
}
