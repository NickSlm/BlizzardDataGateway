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
            var username = registerRequest.Username;
            var password = registerRequest.Password;

            var result = await _authService.SaveUser(username, password);

            if (!result.success)
            {
                return BadRequest(new { message = result.errorMessage });
            }

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserDto loginRequest)
        {
            var username = loginRequest.Username;
            var password = loginRequest.Password;


            var result = await _authService.LoginUser(username, password);

            if (!result.success)
            {
                return Unauthorized("Invalid Credentials");
            }

            return Ok(new
            {
                accessToken = result.token,
                expiresIn = 3600,
                tokenType = "Bearer"
            });
        }

    }
}
