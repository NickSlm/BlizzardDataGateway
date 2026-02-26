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
        public async Task<ActionResult<IEnumerable<LeaderboardSnapshotDto>>> GetSnapshots()
        {

            var snapshots = await _dbService.ListSnapshots();

            if (snapshots == null)
            {
                return NotFound();
            }

            var dtos = snapshots.Select(s => new LeaderboardSnapshotDto
            {
                Id = s.Id,
                DatePulled = s.DatePulled
            });

            return Ok(dtos);
        }

        // GET api/<LeaderboardController>/YY-MM-DD
        [HttpGet("{date}")]
        public async Task<ActionResult<IEnumerable<LeaderboardSnapshotDto>>> GetSnapshotByDate(DateTime date)
        {

            var snapshot = await _dbService.GetSnapshotByDate(date);
            if (snapshot == null)
            {
                return NotFound();
            }

            var dtos = new LeaderboardSnapshotDto
            {
                Id = snapshot.Id,
                DatePulled = snapshot.DatePulled
            };

            return Ok(dtos);
        }
    }
}
