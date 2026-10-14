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
        [HttpPost("RandomBattle")]
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

            return CreatedAtAction(nameof(Get), new { id = Battle.Id }, Battle);
        }
        [HttpPost("{id:length(24)}/Turn")]
        public async Task<ActionResult<Battle>> PostTurn(string id, string move)
        {
            Battle? battle = await _service.GetBattleAsync(id);

            // Request checks
            if (battle is null)
            {
                return NotFound();
            }
            battle.Messages = [];
            if (battle.IsFinished)
            {
                battle.Messages = [$"Battle is finished, winner is {(battle.IsPlayerVictorious ? battle.PlayerPokemon.Name : battle.OpponentPokemon.Name)}"];
                return BadRequest(battle);
            }

            MoveDTO? playerMove = battle.PlayerMoves.FirstOrDefault(m => m.Name == move);

            if (playerMove is null)
            {
                return BadRequest($"{battle.PlayerPokemon.Name} does not know {move}");
            }

            // Pick a random move for the opponent
            Random oppMoveRand = new();
            string oppMoveName = battle.OpponentMoves[oppMoveRand.Next(0, 5)].Name;
            MoveDTO oppMove = battle.OpponentMoves.FirstOrDefault(m => m.Name == oppMoveName)!;

            //TODO: Update how moveDTO is passed in so the actual move that is part of the Battle is updated and not a copy of it.
            if(battle.PlayerPokemon.Speed >= battle.OpponentPokemon.Speed)
            {
                PlayerTurn(ref battle, ref playerMove);
                if (!battle.IsFinished)
                {
                    OpponentTurn(ref battle, ref oppMove);
                }
            }
            else
            {
                OpponentTurn(ref battle, ref oppMove);
                if (!battle.IsFinished)
                {
                    PlayerTurn(ref battle, ref playerMove);
                }
            }
            if (battle.IsFinished)
            {
                await _service.UpdateBattle(battle);
                return Ok(battle);
            }

            await _service.UpdateBattle(battle);
            return Ok(battle);

        }

        private void PlayerTurn(ref Battle battle, ref MoveDTO playerMove)
        {
            //TODO: Check if the move has enough PP
            playerMove.PP -= 1;
            battle.Messages.Add($"{battle.PlayerPokemon} used {playerMove.Name}!");
            int opponentDamage = BattleCalculator.CalculateDamage(playerMove, battle.PlayerPokemon, battle.OpponentPokemon, battle.Messages);

            if (opponentDamage >= battle.OpponentPokemon.HP)
            {
                battle.FinishBattle(true);
            }

            battle.OpponentPokemon.HP -= opponentDamage;
        }

        private void OpponentTurn(ref Battle battle, ref MoveDTO opponentMove)
        {
            //TODO: Check if the move has enough PP
            opponentMove.PP -= 1;
            battle.Messages.Add($"{battle.OpponentPokemon} used {opponentMove.Name}!");
            int playerDamage = BattleCalculator.CalculateDamage(opponentMove, battle.OpponentPokemon, battle.PlayerPokemon, battle.Messages);

            if (playerDamage >= battle.PlayerPokemon.HP)
            {
                battle.FinishBattle(false);
            }

            battle.PlayerPokemon.HP -= playerDamage;
        }
    }
}

