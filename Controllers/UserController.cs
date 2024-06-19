using AutoMapper;
using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service;
using HealthcareAppointmentApp.Service.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
                List<string> errors = new();
                foreach (var entry in ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(ModelState);
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
        [SwaggerOperation(Summary = "Get a user by id")]
        [ProducesResponseType(typeof(UserReadOnlyDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserById(long id)
        {
            var user = await _applicationService.UserService.GetUserByIdAsync(id);
            var returnedUser = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedUser);
        }

        [HttpGet("details")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get all users with their details", Description = "Only admins have access.")]
        [ProducesResponseType(typeof(IList<UserDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status403Forbidden)]
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

        [HttpGet("details/{id}")]
        [Authorize]
        public async Task<ActionResult<UserDetailsDTO>> GetUserDetails(long id)
        {
            if (AppUser!.Role != "Admin" && id != AppUser.Id) throw new ForbiddenActionException("This action is not allowed");
            var userDetails = await _applicationService.UserService.GetUserDetailsById(id);
            var userToReturn = _mapper.Map<UserDetailsDTO>(userDetails);
            return Ok(userToReturn);
        }

        [HttpPatch]
        [Authorize]
        [SwaggerOperation(Summary = "Update a user's email and password", Description = "Only authorized users can access it. Admins can update everyone." +
            "Patients and doctors can only update their account.")]
        [ProducesResponseType(typeof(UserReadOnlyDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadOnlyDTO>> UpdateEmailAndPassword(UserUpdatePasswordEmailDTO dto)
        {
            if (AppUser!.Role != "Admin" && dto.Id != AppUser.Id)
            {
                throw new ForbiddenActionException("This action is not allowed");
            }
            var updatedUser = await _applicationService.UserService.UpdateUserAsync(dto);
            var updatedUserToReturn = _mapper.Map<UserReadOnlyDTO>(updatedUser);
            return Ok(updatedUserToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete a user by ID", Description = "Only authorized users can access it. Admins can delete everyone." +
            "Patients and doctors can only delete their account.")]
        [ProducesResponseType(typeof(UserReadOnlyDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
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
