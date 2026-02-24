using DataGateway.Data;
using DataGateway.DTO;
using DataGateway.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private IDbService _dbService;

        public LeaderboardController(IDbService dbService)
        {
            _dbService = dbService;
        }


        // GET: api/<LeaderboardController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardSnapshotDto>>> Get()
        {

            var snapshots = await _dbService.ListSnapshots();

            var dtos = snapshots.Select(s => new LeaderboardSnapshotDto
            {
                Id = s.Id,
                DatePulled = s.DatePulled
            });


            return Ok(dtos);
        }

        // GET api/<LeaderboardController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LeaderboardController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<LeaderboardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<LeaderboardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
