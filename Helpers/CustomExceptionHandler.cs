using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace HealthcareAppointmentApp.Helpers
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
                Exception exception, CancellationToken cancellationToken)
        {
            var response = httpContext.Response;

            response.StatusCode = exception switch
            {
                DoctorAlreadyExistsException or
                InvalidCredentialsException or
                PatientAlreadyExistsException or
                AccountNotActivatedException or
                AccountAlreadyActivatedException or
                DayAvailabilityAlreadyInsertedException or
                UserAlreadyExistsException => (int)HttpStatusCode.BadRequest,

                UserNotFoundException => (int)HttpStatusCode.NotFound,
                ForbiddenActionException => (int)HttpStatusCode.Forbidden,
                AccountDisabledException => (int)HttpStatusCode.Locked,
                _ => (int)HttpStatusCode.InternalServerError
            };
            await response.WriteAsJsonAsync(new Error { Message = exception.Message }, cancellationToken);
            return true;
        }
    }
}
