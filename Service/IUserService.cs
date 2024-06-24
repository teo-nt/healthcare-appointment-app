using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service.Exceptions;

namespace HealthcareAppointmentApp.Service
{
    public interface IUserService
    {
        /// <summary>
        /// Signs up a user as a patient.
        /// </summary>
        /// <param name="dto">The patient containing all user details.</param>
        /// <returns>The created user</returns>
        /// <exception cref="Exceptions.PatientAlreadyExistsException"
        /// <exception cref="Exceptions.UserAlreadyExistsException"
        Task<User> SignUpUserPatientAsync(PatientSignUpDTO dto);

        /// <summary>
        /// Signs up a user as a doctor.
        /// </summary>
        /// <param name="dto">The doctor containing all user details.</param>
        /// <returns>The created user</returns>
        /// <exception cref="Exceptions.UserAlreadyExistsException"
        /// <exception cref="Exceptions.DoctorAlreadyExistsException"
        Task<User> SignUpUserDoctorAsync(DoctorSignUpDTO dto);

        /// <summary>
        /// Returns the user based on loginDTO (username and password).
        /// </summary>
        /// <param name="loginDTO">The dto containing username and password.</param>
        /// <returns>The user with the credentials provided if valid.</returns>
        /// <exception cref="Exceptions.InvalidCredentialsException"
        Task<User> LoginUserAsync(LoginDTO loginDTO);

        /// <summary>
        /// Updates the user's email and password.
        /// </summary>
        /// <param name="dto">Entity that contains email and password.</param>
        /// <returns>The updated user.</returns>
        /// <exception cref="Exceptions.UserAlreadyExistsException"
        /// <exception cref="Exceptions.UserNotFoundException"
        Task<User> UpdateUserAsync(UserUpdatePasswordEmailDTO dto);

        /// <summary>
        /// Updates a user navigation properties.
        /// </summary>
        /// <param name="dto">The updated user details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        /// <exception cref="Exceptions.UserAlreadyExistsException"
        /// <exception cref="Exceptions.DoctorAlreadyExistsException"
        /// <exception cref="Exceptions.PatientAlreadyExistsException"
        Task UpdateUserDetailsAsync(UserDetailsDTO dto);

        /// <summary>
        /// Returns the user that has the provided username.
        /// </summary>
        /// <param name="username">Username of user to be found.</param>
        /// <returns>The user with the requested username.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Retrieves a user by id.
        /// </summary>
        /// <param name="id">The id of user.</param>
        /// <returns>The <see cref="User"/>.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        Task<User> GetUserByIdAsync(long id); 

        /// <summary>
        /// Creates a JWT with provided claims.
        /// </summary>
        /// <param name="appUser">Entity containing the claims.</param>
        /// <param name="jwtSettings">Configuration from appsettings.json that 
        ///     contains all necessary fields to create a jwt.</param>
        /// <returns>The JWT.</returns>
        string CreateUserToken(ApplicationUser appUser, IConfigurationSection jwtSettings);

        /// <summary>
        /// Deletes a user based on id. Exception is thrown if user is not found.
        /// </summary>
        /// <param name="id">Id of user.</param>
        /// <returns>The deleted user.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        Task<User> DeleteUserAsync(long id);

        /// <summary>
        /// Gets all users with their details.
        /// </summary>
        /// <returns>An IEnumerable of <see cref="User"/>.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Gets a user with details (either doctor or patient).
        /// </summary>
        /// <param name="id">The id of user to be found.</param>
        /// <returns>A user with details included. If no user is found an exception is thrown.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        Task<User> GetUserDetailsById(long id);

        /// <summary>
        /// Activates an account provided the user id.
        /// </summary>
        /// <param name="id">The id of user account to be activated.</param>
        /// <returns>True if activated, otherwise exceptions are thrown.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        /// <exception cref="Exceptions.AccountAlreadyActivatedException"
        Task<bool> EnableAccountById(long id);

        /// <summary>
        /// Disables an account provided the user id.
        /// </summary>
        /// <param name="id">The id of user account to be disabled.</param>
        /// <returns>True if disabled, otherwise exceptions are thrown.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        /// <exception cref="Exceptions.AccountDisabledException"
        Task<bool> DisableAccountById(long id);
    }
}
