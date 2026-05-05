using PokeAPI.Models;
using PokeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace PokeAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class BattleController : ControllerBase
    {
        private readonly PokeAPIService _service;

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Battle>> Get(string id)
        {
            var battle = await _service.GetBattleAsync(id);

            if (battle is null)
            {
                return NotFound();
            }
            return battle;
        }
        [HttpPost]
        public async Task<ActionResult<Battle>> CreateRandomBattle(string playerPokemonName)
        {
            var playerPokemon = await _service.GetPokemonAsync(playerPokemonName);
            var opponentPokemon = await _service.GetPokemonRandomPokemonAsync();

            if (playerPokemon is null || opponentPokemon is null)
            {
                return BadRequest("Pokemon not found");
            }

            var Battle = new Battle { PlayerPokemon = playerPokemon, OpponentPokemon = opponentPokemon };
            _service.CreateBattleAsync(Battle);
            
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult<Battle>> PostMove(Move move)
        {
            throw new NotImplementedException();
        }
    }
}
