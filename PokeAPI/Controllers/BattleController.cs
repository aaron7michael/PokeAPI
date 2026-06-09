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

            var Battle = new Battle(playerPokemon,opponentPokemon );
            _service.CreateBattleAsync(Battle);
            
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ActionResult<Battle>> PostMove(Move move)
        {
            throw new NotImplementedException();
        }
        private PokemonDTO Burn(PokemonDTO pokemon)
        {
            PokemonDTO newPokemonDTO = new PokemonDTO(pokemon);
            // Burn status effect logic
            // For example, reduce HP by 5% each turn and reduce Attack by 50%
            newPokemonDTO.HP = (int)(newPokemonDTO.HP * 0.95);
            // newPokemonDTO.Attack = (int)(newPokemonDTO.Attack * 0.5);
            return newPokemonDTO;
        }
        public PokemonDTO Paralyze(PokemonDTO pokemon)
        {
            PokemonDTO newPokemonDTO = new PokemonDTO(pokemon);
            // Paralyze status effect logic
            // For example, reduce Speed by 50% and have a 25% chance to be unable to move each turn
            newPokemonDTO.Speed = (int)(newPokemonDTO.Speed * 0.5);
            Random rand = new Random();
            if (rand.Next(100) < 25)
            {
                // Pokemon is unable to move this turn
                Console.WriteLine($"{pokemon.Name} is paralyzed and can't move!");
            }
            return newPokemonDTO;
        }
        public void Poison(Pokemon pokemon)
        {
            // Poison status effect logic
            // For example, reduce HP by 5% each turn
            pokemon.HP = (int)(pokemon.HP * 0.95);
        }
        public void Sleep(Pokemon pokemon)
        {
            // Sleep status effect logic
            // For example, have a 50% chance to wake up each turn
            Random rand = new Random();
            if (rand.Next(100) < 50)
            {
                // Pokemon wakes up
                Console.WriteLine($"{pokemon.Name} woke up!");
            }
            else
            {
                // Pokemon is still asleep
                Console.WriteLine($"{pokemon.Name} is still asleep.");
            }
        }
        public void Freeze(Pokemon pokemon)
        {
            // Freeze status effect logic
            // For example, have a 20% chance to thaw out each turn
            Random rand = new Random();
            if (rand.Next(100) < 20)
            {
                // Pokemon thaws out
                Console.WriteLine($"{pokemon.Name} thawed out!");
            }
            else
            {
                // Pokemon is still frozen
                Console.WriteLine($"{pokemon.Name} is still frozen.");
            }
        }
    }
}

