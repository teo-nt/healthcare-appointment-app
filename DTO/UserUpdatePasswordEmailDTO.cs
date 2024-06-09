using System.ComponentModel.DataAnnotations;

namespace HealthcareAppointmentApp.DTO
{
    public class UserUpdatePasswordEmailDTO
    {
        [Required(ErrorMessage = "User id is required")]
        public long id { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$",
            ErrorMessage = "Password should be at least 8 characters containing at least one uppercase and one lowercase letter, " +
            "one digit and one special character")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Password fields don't match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = string.Empty;
    }
}
