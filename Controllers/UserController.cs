using AutoMapper;
using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service;
using HealthcareAppointmentApp.Service.Exceptions;
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

        [HttpPost("login")]
        public async Task<ActionResult<JwtTokenDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _applicationService.UserService.LoginUserAsync(loginDTO);
           /* if (user.Status == UserStatus.Pending)
            {
                throw new AccountNotActivatedException("This account is not activated yet");
            }*/
            var appUser = new ApplicationUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };
            var jwtSettings = _configuration.GetSection("JWTSettings");
            string token = _applicationService.UserService.CreateUserToken(appUser, jwtSettings);

            JwtTokenDTO jwt = new()
            {
                Token = token,
            };

            return Ok(jwt);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserById(long id)
        {
            var user = await _applicationService.UserService.GetUserByIdAsync(id);
            var returnedUser = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedUser);
        }

        [HttpGet("details")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IList<UserDetailsDTO>>> GetAllUsersDetails()
        {
            var users = await _applicationService.UserService.GetAllUsersAsync();
            IList<UserDetailsDTO> usersToReturn = [];
            foreach (var user in users)
            {
                usersToReturn.Add(_mapper.Map<UserDetailsDTO>(user));
            }
            return Ok(usersToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<UserReadOnlyDTO>> DeleteUserById(long id)
        {
            if (AppUser!.Role != "Admin" && AppUser.Id != id)
            {
                throw new ForbiddenActionException("This action is not allowed");
            }
            var user = await _applicationService.UserService.DeleteUserAsync(id);
            var returnedUser = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedUser);
        }
    }
}
