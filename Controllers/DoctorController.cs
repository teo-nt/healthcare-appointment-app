using AutoMapper;
using FluentValidation;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HealthcareAppointmentApp.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IValidator<AvailabilityInsertDTO> _validator;

        public DoctorController(IApplicationService applicationService, IMapper mapper,
            IValidator<AvailabilityInsertDTO> validator) : base(applicationService)
        {
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("availability")]
        [Authorize(Roles = "Doctor")]
        [ProducesResponseType(typeof(IList<AvailableTimeslotDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IList<AvailableTimeslotDTO>>> AddAvailability(AvailabilityInsertDTO dto)
        {
            var validationResults = _validator.Validate(dto);
            var errors = new List<Error>();
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    errors.Add(new Error { Message = error.ErrorMessage });
                }
                return BadRequest(errors);
            }

            var timeslots = await _applicationService.DoctorService.AddAvailabilityAsync(dto, AppUser!.Id);
            IList<AvailableTimeslotDTO> timeslotsToReturn = [];
            foreach (var timeslot in timeslots)
            {
                timeslotsToReturn.Add(_mapper.Map<AvailableTimeslotDTO>(timeslot));
            }
            return Ok(timeslotsToReturn);
        }

        [HttpGet("timeslots")]
        [Authorize(Roles = "Doctor")]
        [SwaggerOperation(Summary = "Gets all timeslots for logged in doctor",
                Description = "Only users logged in as doctors have access.")]
        [ProducesResponseType(typeof(IList<AvailableTimeslotDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IList<AvailableTimeslotDTO>>> GetTimeSlots()
        {
            var timeslots = await _applicationService.DoctorService.GetTimeslotsByUserId(AppUser!.Id);
            IList<AvailableTimeslotDTO> timeslotsToReturn = [];
            foreach (var timeslot in timeslots)
            {
                timeslotsToReturn.Add(_mapper.Map<AvailableTimeslotDTO>(timeslot));
            }
            return Ok(timeslotsToReturn);
        }

        [HttpPost("search")]
        [Authorize]
        [SwaggerOperation(Summary = "Gets all doctors with a specific speciality and city",
                Description = "Only authorized users have access.")]
        [ProducesResponseType(typeof(IList<DoctorDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<DoctorDTO>>> GetDoctorsBySpecialityAndCity(DoctorRequestDTO dto)
        {
            var doctors = await _applicationService.DoctorService.GetBySpecialityAndCity(dto);
            IList<DoctorDTO> doctorsToReturn = [];
            foreach (var doctor in doctors)
            {
                doctorsToReturn.Add(_mapper.Map<DoctorDTO>(doctor));
            }
            return Ok(doctorsToReturn);
        }
    }
}
