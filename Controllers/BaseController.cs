using HealthcareAppointmentApp.Models;
using HealthcareAppointmentApp.Service;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HealthcareAppointmentApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly IApplicationService _applicationService;
        private ApplicationUser? _appUser;

        protected BaseController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        protected ApplicationUser? AppUser
        {
            get
            {
                if (User is null || User.Claims is null) return null;
                var claimTypes = User.Claims.Select(x => x.Type);            
                if (!claimTypes.Contains(ClaimTypes.NameIdentifier)) return null;
                if (!claimTypes.Contains(ClaimTypes.Name)) return null;
                if (!claimTypes.Contains(ClaimTypes.Email)) return null;
                if (!claimTypes.Contains(ClaimTypes.Role)) return null;
                var userClaimsId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userClaimsUsername = User.FindFirstValue(ClaimTypes.Name);
                var userClaimsEmail = User.FindFirstValue(ClaimTypes.Email);
                var userClaimsRole = User.FindFirstValue(ClaimTypes.Role);
                _ = long.TryParse(userClaimsId, out long id);
                
                _appUser = new ApplicationUser
                {
                    Id = id,
                    Username = userClaimsUsername!,
                    Email = userClaimsEmail!,
                    Role = userClaimsRole!
                };

                return _appUser;
            }
        }
    }
}
