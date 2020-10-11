
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace VehicleTracking.Controllers
{
    public class BaseApiController : ControllerBase
    {

        /// <summary>
        /// Property for future use.
        /// </summary>
        public string UserFullName => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
        public string UserEmail => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

    }
}