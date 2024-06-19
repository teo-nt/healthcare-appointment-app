using System.ComponentModel.DataAnnotations;

namespace HealthcareAppointmentApp.DTO
{
    public class AvailabilityInsertDTO
    {
        [Required(ErrorMessage = "Date is required")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public TimeOnly EndTime { get; set; }
    }
}
