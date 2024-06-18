using AutoMapper;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentApp.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IMapper _mapper;

        public DoctorController(IApplicationService applicationService, IMapper mapper) : base(applicationService)
        {
            _mapper = mapper;
        }

        [HttpPost("availability")]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<IList<AvailableTimeslotDTO>>> AddAvailability(AvailabilityInsertDTO dto)
        {
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
