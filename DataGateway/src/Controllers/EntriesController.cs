using DataGateway.DTO;
using DataGateway.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {

        private IDbService _dbService;
        public EntriesController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("snapshot/{snapshotId}")]
        public async Task<ActionResult<IEnumerable<LeaderboardEntriesDto>>> GetEntries(int snapshotId)
        {
            var entriesList = await _dbService.GetSnapshotEntries(snapshotId);
            var dtos = entriesList.Select(e => new LeaderboardEntriesDto
            {
                CharacterName = e.CharacterName,
                Played = e.Played,
                Lost = e.Lost,
                Won = e.Won,
                Rank = e.Rank,
                Rating = e.Rating

            });

            return Ok(dtos);
        }

        [HttpGet("character/{characterName}")]
        public async Task<ActionResult<LeaderboardEntriesDto>> GetCharacter(string characterName)
        {
            var character = await _dbService.GetCharacter(characterName);
            var dto = new LeaderboardEntriesDto
            {
                CharacterName = character.CharacterName,
                Played = character.Played,
                Lost = character.Lost,
                Won = character.Won,
                Rank = character.Rank,
                Rating = character.Rating
            };

            return Ok(dto);

        }


    }
}
