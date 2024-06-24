using AutoMapper;
using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Repositories;
using HealthcareAppointmentApp.Security;
using HealthcareAppointmentApp.Service.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthcareAppointmentApp.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public string CreateUserToken(ApplicationUser appUser, IConfigurationSection jwtSettings)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.GetSection("securityKey").Value!));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUser.Username),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Role, appUser.Role),
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Aud, jwtSettings.GetSection("ValidAudience").Value!),
                new Claim(JwtRegisteredClaimNames.Iss, jwtSettings.GetSection("ValidIssuer").Value!)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> DeleteUserAsync(long id)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.DeleteAsync(id);
                if (user is null) throw new UserNotFoundException($"User with id {id} does not exist");
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"User with id: {id} was deleted");
                return user;
            }
            catch (UserNotFoundException e)
            {
                _logger.LogWarning($"Error in delete -- {e.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();
            _logger.LogInformation("All users with their details were retrieved");
            return users;
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetAsync(id);
                if (user is null) throw new UserNotFoundException($"User with id {id} does not exist");
                _logger.LogInformation($"User with id: {id} was retrieved");
                return user;
            }
            catch (UserNotFoundException e)
            {
                _logger.LogWarning($"Error in retrieving user -- {e.Message}");
                throw;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetByUsernameAsync(username);
                if (user is null) throw new UserNotFoundException($"User with username {username} does not exist");
                _logger.LogInformation($"User with username: {username} was retrieved");
                return user;
            }
            catch (UserNotFoundException e)
            {
                _logger.LogWarning($"Error in retrieving user -- {e.Message}");
                throw;
            }
        }

        public async Task<User> GetUserDetailsById(long id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetDetailsAsync(id);
                if (user is null) throw new UserNotFoundException($"User with id: {id} does not exist");
                _logger.LogInformation($"User details with id: {id} were retrieved");
                return user;
            }
            catch (UserNotFoundException e)
            {
                _logger.LogWarning($"Error in getting user details -- {e.Message}");
                throw;
            }
        }

        public async Task<User> LoginUserAsync(LoginDTO loginDTO)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetUserAsync(loginDTO.Username, loginDTO.Password);
                if (user is null) throw new InvalidCredentialsException
                        ($"Invalid credentials - Failure in login with username: {loginDTO.Username}");
                _logger.LogInformation($"User with username: {loginDTO.Username} logged in");
                return user;
            }
            catch (InvalidCredentialsException e)
            {
                _logger.LogWarning($"Error in user login -- {e.Message}");
                throw;
            }
        }

        public async Task<User> SignUpUserDoctorAsync(DoctorSignUpDTO dto)
        {
            User? user;
            Doctor? doctor;
            Speciality? speciality;
            string specialityName = dto.Speciality;

            try
            {
                user = ExtractUser(dto);
                User? existingUser = await _unitOfWork.UserRepository.GetByUsernameOrEmailAsync(user.Username, user.Email);
                if (existingUser is not null) throw new UserAlreadyExistsException
                        ($"User with username: {user.Username} or email: {user.Email} already exists");
                user.Password = EncryptionUtil.EncryptPassword(user.Password);
                user.UserRole = UserRole.Doctor;
                user.Status = UserStatus.Pending;
                
                doctor = ExtractDoctor(dto);
                Doctor? existingDoctor = await _unitOfWork.DoctorRepository.GetDoctorByPhoneNumber(doctor.PhoneNumber);
                if (existingDoctor is not null) throw new DoctorAlreadyExistsException
                        ($"Doctor with phone number: {doctor.PhoneNumber} already exists");

                Speciality? existingSpeciality = await _unitOfWork.SpecialityRepository.GetSpecialityByNameAsync(specialityName);
                if (existingSpeciality is null) 
                {
                    speciality = await _unitOfWork.SpecialityRepository.AddAsync(new Speciality { SpecialityName = specialityName });
                }
                else
                {
                    speciality = existingSpeciality;
                }
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.DoctorRepository.AddAsync(doctor);
                doctor.User = user;
                doctor.Speciality = speciality;
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"Doctor with username: {user.Username} signed up successfully.");
                return user;
            }
            catch (Exception e) when (e is UserAlreadyExistsException || e is DoctorAlreadyExistsException)
            {
                _logger.LogWarning($"Error signing up doctor -- {e.Message}");
                throw;
            }
        }

        public async Task<User> SignUpUserPatientAsync(PatientSignUpDTO dto)
        {
            User? user;
            Patient? patient;

            try
            {
                user = ExtractUser(dto);
                User? existingUser = await _unitOfWork.UserRepository.GetByUsernameOrEmailAsync(user.Username, user.Email);
                if (existingUser is not null) throw new UserAlreadyExistsException
                        ($"User with username: {user.Username} or email: {user.Email} already exists");
                user.Password = EncryptionUtil.EncryptPassword(user.Password);
                user.UserRole = UserRole.Patient;
                user.Status = UserStatus.Approved;

                patient = ExtractPatient(dto);
                Patient? existingPatient = await _unitOfWork.PatientRepository.GetPatientByPhoneNumber(patient.PhoneNumber);
                if (existingPatient is not null) throw new PatientAlreadyExistsException
                        ($"Patient with phone number: {patient.PhoneNumber} already exists");
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.PatientRepository.AddAsync(patient);
                patient.User = user;
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"Patient with username: {user.Username} signed up successfully.");
                return user;
            }
            catch (Exception e) when (e is PatientAlreadyExistsException || e is UserAlreadyExistsException)
            {
                _logger.LogWarning($"Error signing up patient -- {e.Message}");
                throw;
            }
        }

        public async Task<User> UpdateUserAsync(UserUpdatePasswordEmailDTO dto)
        {
            User? user;

            try
            {
                user = await _unitOfWork.UserRepository.GetAsync(dto.Id);
                if (user is null) throw new UserNotFoundException($"User with id: {dto.Id} does not exist");
                User? existingUserEmail = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
                if (existingUserEmail is not null && existingUserEmail.Id != dto.Id)
                {
                    throw new UserAlreadyExistsException($"User with email: {dto.Email} already exists");
                }
                user.Password = EncryptionUtil.EncryptPassword(dto.Password);
                user.Email = dto.Email;
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"User with username: {user.Username} was updated successfully");
                return user;
            }
            catch (Exception e) when (e is UserNotFoundException || e is UserAlreadyExistsException)
            {
                _logger.LogWarning($"Error updating user -- {e.Message}");
                throw;
            }
        }


        public async Task UpdateUserDetailsAsync(UserDetailsDTO dto)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetDetailsAsync(dto.Id);
                if (user is null) throw new UserNotFoundException($"User with id: {dto.Id} does not exist.");
                _unitOfWork.UserRepository.Detach(user);

                User? existingUsernameUser = await _unitOfWork.UserRepository.GetByUsernameAsync(dto.Username);
                if (existingUsernameUser is not null && existingUsernameUser.Id != dto.Id) 
                        throw new UserAlreadyExistsException($"User with username: {dto.Username} already exists");
                if (existingUsernameUser is not null) _unitOfWork.UserRepository.Detach(existingUsernameUser);

                User? existingEmailUser = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
                if (existingEmailUser is not null && existingEmailUser.Id != dto.Id)
                    throw new UserAlreadyExistsException($"User with email: {dto.Email} already exists");
                if (existingEmailUser is not null) _unitOfWork.UserRepository.Detach(existingEmailUser);

                User userToUpdate = _mapper.Map<User>(dto);
                userToUpdate.Password = user.Password;

                if (dto.UserRole is UserRole.Patient)
                {
                    Patient? existingPatient = await _unitOfWork.PatientRepository.GetPatientByPhoneNumber(dto.Patient!.PhoneNumber);
                    if (existingPatient is not null && existingPatient.Id != dto.Patient.Id)
                        throw new PatientAlreadyExistsException($"Patient with phone number {dto.Patient.PhoneNumber} already exists");
                    if (existingPatient is not null) _unitOfWork.PatientRepository.Detach(existingPatient);
                } else if (dto.UserRole is UserRole.Doctor)
                {
                    Doctor? existingDoctor = await _unitOfWork.DoctorRepository.GetDoctorByPhoneNumber(dto.Doctor!.PhoneNumber);
                    if (existingDoctor is not null && existingDoctor.Id != dto.Doctor.Id)
                        throw new DoctorAlreadyExistsException($"Doctor with phone number {dto.Doctor.PhoneNumber} already exists");
                    if (existingDoctor is not null) _unitOfWork.DoctorRepository.Detach(existingDoctor);
                    Speciality? existingSpeciality = await _unitOfWork.SpecialityRepository
                                                            .GetSpecialityByNameAsync(dto.Doctor.Speciality!.SpecialityName);
                    if (existingSpeciality is null)
                    {
                        user.Doctor!.Speciality!.Id = 0;
                    }
                    else
                    {
                        _unitOfWork.SpecialityRepository.Detach(existingSpeciality);
                        user.Doctor!.Speciality = existingSpeciality;
                    }
                }
                _unitOfWork.UserRepository.UpdateDetails(userToUpdate);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"User with id: {dto.Id} was updated");
            }
            catch (Exception e) when (e is UserNotFoundException or 
                                           UserAlreadyExistsException or 
                                           DoctorAlreadyExistsException or
                                           PatientAlreadyExistsException
                                      )
            {
                _logger.LogWarning($"Error updating user -- {e.Message}");
                throw;
            } 
        }

        public async Task<bool> EnableAccountById(long id)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(id);
                if (user is null) throw new UserNotFoundException($"User with id: {id} does not exist");
                if (user.Status is UserStatus.Approved) 
                        throw new AccountAlreadyActivatedException($"User account with id: { id } is already activated");
                user.Status = UserStatus.Approved;
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"User account with id: {id} was activated");
            }
            catch (Exception e) when (e is UserNotFoundException || e is AccountAlreadyActivatedException)
            {
                _logger.LogWarning($"Error enabling account -- {e.Message}");
                throw;
            }
            return true;
        }


        public async Task<bool> DisableAccountById(long id)
        {
            try
            {
                User? user = await _unitOfWork.UserRepository.GetAsync(id);
                if (user is null) throw new UserNotFoundException($"User with id: {id} does not exist");
                if (user.Status is UserStatus.Disabled)
                    throw new AccountDisabledException($"User account with id: {id} is already deactivated");
                user.Status = UserStatus.Disabled;
                await _unitOfWork.SaveAsync();
                _logger.LogInformation($"User account with id: {id} was deactivated");
            }
            catch (Exception e) when (e is UserNotFoundException || e is AccountDisabledException)
            {
                _logger.LogWarning($"Error disabling account -- {e.Message}");
                throw;
            }
            return true;
        }

        private User ExtractUser(UserSignUpDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email
            };
        }

        private Patient ExtractPatient(PatientSignUpDTO dto)
        {
            return new Patient
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Age = dto.Age,
                Gender = dto.Gender,
                MedicalHistory = dto.MedicalHistory,
                PhoneNumber = dto.PhoneNumber
            };
        }

        private Doctor ExtractDoctor(DoctorSignUpDTO dto)
        {
            const uint APPOINTMENT_DEFAULT_DURATION = 30;
            return new Doctor
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                City = dto.City,
                Address = dto.Address,
                StreetNumber = dto.StreetNumber,
                PhoneNumber = dto.PhoneNumber,
                AppointmentDuration = dto.AppointmentDuration ?? APPOINTMENT_DEFAULT_DURATION
            };
        }

    }
}
