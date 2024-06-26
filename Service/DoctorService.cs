﻿using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Repositories;
using HealthcareAppointmentApp.Service.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareAppointmentApp.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IUnitOfWork unitOfWork, ILogger<DoctorService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Adds availability for doctor, producing timeslots 
        /// according to doctor's appointment duration.
        /// </summary>
        /// <param name="dto">Entity containing the available time period of doctor for a specific day.</param>
        /// <param name="id">The user id of doctor who adds the availability.</param>
        /// <returns>A list of timeslots inserted.</returns>
        /// <exception cref="UserNotFoundException"
        /// <exception cref="DayAvailabilityAlreadyInsertedException"
        public async Task<IList<TimeSlot>> AddAvailabilityAsync(AvailabilityInsertDTO dto, long id)
        {
            IList<TimeSlot> timeslotsToInsert = [];

            try
            {
                User? userDoctor = await _unitOfWork.UserRepository.GetDetailsAsync(id);
                if (userDoctor is null || userDoctor.Doctor is null)
                {
                    throw new UserNotFoundException($"User with id: {id} was not found or is not a doctor");
                }
                var duration = userDoctor.Doctor.AppointmentDuration;

                // Check for coflict with existing timeslots at the same day
                var timeslots = (await _unitOfWork.DoctorRepository.GetTimeSlots(userDoctor.Doctor.Id)).Where(t => t.Date == dto.Date);               
                if (timeslots.Any())
                {
                    throw new DayAvailabilityAlreadyInsertedException
                        ($"Availability for date: {dto.Date} has been already added");
                }
                for (var t = dto.StartTime; t.AddMinutes(duration) <= dto.EndTime; t = t.AddMinutes(duration))
                {
                    timeslotsToInsert.Add(new TimeSlot()
                    {
                        Date = dto.Date,
                        StartTimeSlot = t,
                        EndTimeSlot = t.AddMinutes(duration),
                        Status = AvailabilityStatus.Available,
                        Doctor = userDoctor.Doctor
                    });
                }
                await _unitOfWork.TimeSlotRepository.AddRangeAsync(timeslotsToInsert);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"Doctor with id: {userDoctor.Doctor.Id} successfully added new availability for date: {dto.Date}");
                return timeslotsToInsert;
            }
            catch (Exception e) when (e is UserNotFoundException || e is DayAvailabilityAlreadyInsertedException) 
            {
                _logger.LogWarning($"Error at adding availability for doctor with user id: {id} -- {e.Message}");
                throw;
            }        
        }

        /// <summary>
        /// Retrieve doctors by speciality and city.
        /// </summary>
        /// <param name="dto">Contains speciality and city fields.</param>
        /// <returns><see cref="IEnumerable{Doctor}"/></returns>
        /// <exception cref="Exceptions.DoctorNotFoundException">If no doctors are found with these criteria.</exception> 
        public async Task<IEnumerable<Doctor>> GetBySpecialityAndCity(DoctorRequestDTO dto)
        {
            try
            {
                var doctors = await _unitOfWork.DoctorRepository.GetDoctorsByCityAndSpeciality(dto.City, dto.Speciality);
                if (doctors.IsNullOrEmpty()) 
                    throw new DoctorNotFoundException($"Doctors with speciality: {dto.Speciality} at city: {dto.City} were not found");
                _logger.LogInformation($"Doctors with speciality: {dto.Speciality} at city: {dto.City} were retrieved");
                return doctors;
            }
            catch (Exception e) when (e is DoctorNotFoundException)
            {
                _logger.LogWarning($"Error getting doctors -- {e.Message}");
                throw;
            }      
        }

        public async Task<IEnumerable<TimeSlot>> GetFutureAvailableTimeslotsByDoctorId(long doctorId)
        {
            try
            {
                var timeslots = await _unitOfWork.DoctorRepository.GetFutureAvailableTimeslotsByDoctorId(doctorId);
                if (timeslots.IsNullOrEmpty()) 
                    throw new TimeslotNotFoundException($"No future available timeslots were found for doctor with id: {doctorId}");
                _logger.LogInformation($"Future available timeslots for doctor with id: {doctorId} were retrieved");
                return timeslots;
            }
            catch (Exception e) when (e is TimeslotNotFoundException)
            {
                _logger.LogWarning($"Error getting timeslots -- {e.Message}");
                throw;
            }     
        }

        /// <summary>
        /// Gets all timeslots for a user who must be a doctor.
        /// </summary>
        /// <param name="userId">The id of user.</param>
        /// <returns>An IEnumerable of <see cref="TimeSlot"/> for the specific doctor.</returns>
        /// <exception cref="UserNotFoundException">If user is not found or is not a doctor.</exception>
        public async Task<IEnumerable<TimeSlot>> GetTimeslotsByUserId(long userId)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetDetailsAsync(userId);
                if (user is null || user.Doctor is null) 
                    throw new UserNotFoundException($"User with id: {userId} was not found or is not a doctor");
                var timeslots = await _unitOfWork.DoctorRepository.GetTimeSlots(user.Doctor.Id);
                _logger.LogInformation($"Doctor with id: {user.Doctor.Id} retreived timeslots successfully");
                return timeslots;
            }
            catch (Exception e) when (e is UserNotFoundException)
            {
                _logger.LogWarning($"Error at getting timeslots -- {e.Message}");
                throw;
            }
        }
    }
}
