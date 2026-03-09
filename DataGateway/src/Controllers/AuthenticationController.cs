using DataGateway.DTO;
using DataGateway.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserDto registerRequest)
        {
            //:TODO Auth Logic
            var username = registerRequest.Username;
            var password = registerRequest.Password;



            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDto loginRequest)
        {
            //:TODO Authorization Logic

            string AccessToken = "PlaceHolder";
            return Ok(AccessToken);
        }

    }
}
