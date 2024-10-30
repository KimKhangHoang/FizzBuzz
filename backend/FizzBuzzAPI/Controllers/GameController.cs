using Microsoft.AspNetCore.Mvc;
using FizzBuzzAPI.Interfaces;
using FizzBuzzAPI.Models;

namespace FizzBuzzAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // POST: api/game/create
        [HttpPost("create")]
        public async Task<ActionResult<GameRule>> CreateGameRule([FromBody] GameRule gameRule)
        {
            if (gameRule == null)
            {
                return BadRequest("Game rule has not been created.");
            }

            await _gameService.CreateGame(gameRule);
            return CreatedAtAction(nameof(GetGameById), new { id = gameRule.Id }, gameRule);
        }

        // GET: api/game/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GameRule>> GetGameById(int id)
        {
            var gameRule = await _gameService.GetGameById(id);

            if (gameRule == null)
            {
                return NotFound();
            }

            return Ok(gameRule);
        }

        // DELETE: api/game/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameRule(int id)
        {
            var gameRule = await _gameService.GetGameById(id);

            if (gameRule == null)
            {
                return NotFound();
            }

            await _gameService.DeleteGame(id);
            return NoContent();
        }

        // POST: api/game/{id}/addrule
        [HttpPost("{id}/addrule")]
        public IActionResult AddRuleToGame(int id, [FromBody] Rule newRule)
        {
            if (newRule == null)
            {
                return BadRequest("Rule cannot be null.");
            }

            _gameService.AddRuleToGame(id, newRule.Divisor, newRule.Replacement);
            return NoContent();
        }

        // POST: api/game/random
        [HttpPost("random")]
        public async Task<IActionResult> StartGame([FromBody] int gameId)
        {
            var gameRules = await _gameService.GetGameById(gameId);
            if (gameRules == null) { return NotFound("Game not found."); }
            var randomNumber = new Random().Next(1, gameRules.Range + 1);
            return Ok(new { RandomNumber = randomNumber });
        }

        // POST: api/game/verify
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAnswer(int gameId, string playerInput)
        {
            var result = await _gameService.VerifyAnswer(gameId, playerInput);
            return Ok(new { Result = result });
        }
    }
}
