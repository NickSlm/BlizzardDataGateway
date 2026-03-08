using DataGateway.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerRequest)
        {

            //TODO Auth logic

            return Ok(new { message = "User registered successfully" });
        }

    }
}
