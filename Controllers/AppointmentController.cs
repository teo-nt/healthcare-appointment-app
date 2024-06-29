using AutoMapper;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HealthcareAppointmentApp.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IMapper _mapper;

        public AppointmentController(IApplicationService applicationService, IMapper mapper) : base(applicationService)
        {
            _mapper = mapper;
        }

        [HttpPost("book")]
        [Authorize]
        [SwaggerOperation(Summary = "Book an appointment", 
            Description = "Book an apppointment providing doctor id, user id of patient and timeslot id which contains the information about datetime." +
            "Only authorized users have access to this endpoint.")]
        [ProducesResponseType(typeof(AppointmentReadOnlyDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AppointmentReadOnlyDTO>> BookAppointment(AppointmentRequestDTO dto)
        {
            var appointment = await _applicationService.AppointmentService.BookAppointment(dto);
            AppointmentReadOnlyDTO appointmentToReturn = _mapper.Map<AppointmentReadOnlyDTO>(appointment);
            return Ok(appointmentToReturn);
        }
    }
}
