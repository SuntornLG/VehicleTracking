
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace VehicleTracking.Controllers
{
    [Route("v1/User")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// User registation
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserRegisterResponseDto), 200)]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequestDto user)
        {
            var result = await _userService.CreateAsync(user, user.Password);
            return Ok(result);

        }


        /// <summary>
        /// Authentication user
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(UserResponseDto), 200)]
        public async Task<IActionResult> Authenticate([FromBody]UserLoginDto userLogin)
        {
            var user = await _userService.AuthenticateAsync(userLogin.Email, userLogin.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}