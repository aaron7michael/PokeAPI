using PokeAPI.Models;
using PokeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using PokeAPI.DTOs;

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

            var Battle = new Battle(playerPokemon, opponentPokemon);
            await _service.CreateBattleAsync(Battle);

            return CreatedAtAction(nameof(Get), new { id = Battle.Id }, Battle);)
        }
        [HttpPost("{id:length(24)}")]
        public async Task<ActionResult<Battle>> PostMove(string id, string playerMoveName)
        {
            Battle battle = await _service.GetBattleAsync(id);
            if(battle is null)
            {
                return NotFound();
            }
            Move playerMove = await _service.GetMoveByNameAsync(playerMoveName);
            if(playerMove == null)
            {
                return BadRequest($"Move {playerMoveName} not found");
            }
            if (!battle.PlayerMoves.Select(m => m.Name).Contains(playerMoveName))
            {
                return BadRequest($"{battle.PlayerPokemon.Name} does not know {playerMoveName}");
            }
            int damage = BattleCalculator.CalculateDamage(playerMove, battle.PlayerPokemon, battle.OpponentPokemon);
            
            // Implementation for posting a move
        }
        
    }
}

