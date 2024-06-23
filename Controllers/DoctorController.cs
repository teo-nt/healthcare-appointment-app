using AutoMapper;
using FluentValidation;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    }
}
