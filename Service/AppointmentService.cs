using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Repositories;
using HealthcareAppointmentApp.Service.Exceptions;

namespace HealthcareAppointmentApp.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IUnitOfWork unitOfWork, ILogger<AppointmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Appointment> BookAppointment(AppointmentRequestDTO dto)
        {
            try
            {
                var patientUser = await _unitOfWork.UserRepository.GetDetailsAsync(dto.PatientUserId);
                if (patientUser == null || patientUser.Patient is null)
                    throw new UserNotFoundException($"User with id: {dto.PatientUserId} does not exist or is not a patient");
                var doctor = await _unitOfWork.DoctorRepository.GetAsync(dto.DoctorId);
                if (doctor is null) throw new DoctorNotFoundException($"Doctor with id: {dto.DoctorId} does not exist");
                var timeslot = await _unitOfWork.TimeSlotRepository.GetAsync(dto.TimeslotId);
                if (timeslot is null) throw new TimeslotNotFoundException($"Timeslot with id: {dto.TimeslotId} does not exist");
                if (timeslot.DoctorId != dto.DoctorId || timeslot.Status == AvailabilityStatus.Unavailable) 
                    throw new ForbiddenActionException($"This timeslot does not belong to this doctor or is not available");
                Appointment appointment = new()
                {
                    DateTime = new DateTime(timeslot.Date, timeslot.StartTimeSlot),
                    AppointmentStatus = AppointmentStatus.Pending,
                    Patient = patientUser.Patient,
                    Doctor = doctor
                };
                await _unitOfWork.AppointmentRepository.AddAsync(appointment);
                timeslot.Status = AvailabilityStatus.Unavailable;
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"Appointment with id: {appointment.Id} was created successfully");
                return appointment;
            }
            catch (Exception e) when (e is UserNotFoundException or 
                                            DoctorNotFoundException or 
                                            TimeslotNotFoundException or
                                            ForbiddenActionException)
            {
                _logger.LogWarning($"Error creating appointment -- {e.Message}");
                throw;
            }
        }
    }
}
