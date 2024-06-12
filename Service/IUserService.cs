using HealthcareAppointmentApp.Data;
using HealthcareAppointmentApp.DTO;
using HealthcareAppointmentApp.Models;

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
        /// <param name="userId">Id of user.</param>
        /// <param name="username">Username of user.</param>
        /// <param name="email">Email of user.</param>
        /// <param name="role">Role of user.</param>
        /// <param name="securityKey">Key used for signature.</param>
        /// <returns>The JWT.</returns>
        string CreateUserToken(int userId, string username, string email, UserRole role, String securityKey);

        /// <summary>
        /// Deletes a user based on id. Exception is thrown if user is not found.
        /// </summary>
        /// <param name="id">Id of user.</param>
        /// <returns>The deleted user.</returns>
        /// <exception cref="Exceptions.UserNotFoundException"
        Task<User> DeleteUserAsync(int id);
    }
}
