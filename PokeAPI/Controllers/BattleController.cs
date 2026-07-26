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
        public async Task<ActionResult<Battle>> PostMove(string id, string move)
        { 
            //TODO: Update battle to track current PP for moves.

            Battle battle = await _service.GetBattleAsync(id);
            
            // Request checks
            if(battle is null)
            {
                return NotFound();
            }
            battle.Messages = [];
            if (battle.IsFinished)
            {
                battle.Messages = [ $"Battle is finished, winner is {(battle.IsPlayerVictorious ? battle.PlayerPokemon.Name : battle.OpponentPokemon.Name)}" ];
                return BadRequest(battle);
            }
            
            // Player Turn
            Move playerMove = await _service.GetMoveByNameAsync(move);
            if(playerMove == null)
            {
                return BadRequest($"Move {move} not found");
            }
            if (!battle.PlayerMoves.Select(m => m.Name).Contains(move))
            {
                return BadRequest($"{battle.PlayerPokemon.Name} does not know {move}");
            }

            battle.Messages.Add($"{battle.PlayerPokemon} used {move}!");
            
            int opponentDamage = BattleCalculator.CalculateDamage(playerMove, battle.PlayerPokemon, battle.OpponentPokemon, battle.Messages);
            
            if(opponentDamage >= battle.OpponentPokemon.HP)
            {
                battle.Messages.Add($"{battle.OpponentPokemon.Name} fainted.");

                battle.IsPlayerVictorious = true;
                battle.IsFinished = true;
                battle.OpponentPokemon.HP = 0;

                battle.Messages.Add($"{battle.PlayerPokemon.Name} Wins!");

                return Ok(battle);
            }

            battle.OpponentPokemon.HP -= opponentDamage;
            battle.PlayerMoves.First(m => m.Name == move).PP -= 1;

            // Opponent Turn

            // Pick a random move and use it
            Random oppMoveRand = new();
            string oppMoveName = battle.OpponentMoves[oppMoveRand.Next(0, 5)].Name;
            Move oppMove = await _service.GetMoveByNameAsync(oppMoveName);

            battle.Messages.Add($"{battle.OpponentPokemon} used {oppMoveName}!");

            int playerDamage = BattleCalculator.CalculateDamage(oppMove, battle.OpponentPokemon, battle.PlayerPokemon, battle.Messages);

            if (playerDamage >= battle.PlayerPokemon.HP)
            {
                battle.Messages.Add($"{battle.PlayerPokemon.Name} fainted.");

                battle.IsPlayerVictorious = false;
                battle.IsFinished = true;
                battle.PlayerPokemon.HP = 0;

                battle.Messages.Add($"{battle.OpponentPokemon.Name} Wins!");

                return Ok(battle);
            }

            battle.PlayerPokemon.HP -= playerDamage;
            battle.OpponentMoves.First(m => m.Name == oppMoveName).PP -= 1;

            await _service.UpdateBattle(battle);
            return Ok(battle);
            
        }
        
    }
}

