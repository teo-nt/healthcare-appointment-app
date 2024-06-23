using FluentValidation;
using HealthcareAppointmentApp.DTO;

namespace HealthcareAppointmentApp.Validators
{
    public class AvailabilityInsertValidator : AbstractValidator<AvailabilityInsertDTO>
    {
        public AvailabilityInsertValidator()
        {
            RuleFor(dto => dto.Date)
                .NotEmpty().WithMessage("Date is required")
                .Must(BeFutureDate).WithMessage("Date must be in future");

            RuleFor(dto => dto.StartTime)
                .NotEmpty().WithMessage("StartTime is required");

            RuleFor(dto => dto.EndTime)
                .NotEmpty().WithMessage("EndTime is required")
                .GreaterThan(dto => dto.StartTime).WithMessage("End time must be greater than StartTime");
        }

        private bool BeFutureDate(DateOnly date)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            return date > currentDate;
        }
    }
}
