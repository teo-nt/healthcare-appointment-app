using AutoMapper;
using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IApplicationService applicationService, IConfiguration configuration, IMapper mapper)
            : base(applicationService)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("register-doctor")]
        public async Task<ActionResult<DoctorReadOnlyDTO>> SignUpDoctor(DoctorSignUpDTO signUpDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                return BadRequest(new { Errors = errors });
            }

            User user = await _applicationService.UserService.SignUpUserDoctorAsync(signUpDTO);
            var responseDTO = _mapper.Map<DoctorReadOnlyDTO>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id}, responseDTO);
        }

        [HttpPost("register-patient")]
        public async Task<ActionResult<PatientReadOnlyDTO>> SignUpPatient(PatientSignUpDTO signUpDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                return BadRequest(new { Errors = errors });
            }

            User user = await _applicationService.UserService.SignUpUserPatientAsync(signUpDTO);
            var responseDTO = _mapper.Map<PatientReadOnlyDTO>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, responseDTO);
        }

        /*[HttpPost]
        public async Task<ActionResult<JwtTokenDTO>> Login(LoginDTO loginDTO)
        {

        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserById(long id)
        {
            var user = await _applicationService.UserService.GetUserByIdAsync(id);
            var returnedUser = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedUser);
        }
    }
}
