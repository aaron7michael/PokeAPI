using PokeAPI.Models;
using PokeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace PokeAPI.Controllers;

[ApiController]
[Route("/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokeAPIService _service;

    public PokemonController(PokeAPIService pokemonService) =>
        _service = pokemonService;

    [HttpGet]
    public async Task<List<Pokemon>> Get() =>
        await _service.GetPokemonAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Pokemon>> Get(string id)
    {
        var pokemon = await _service.GetPokemonAsync(id);

        if (pokemon is null)
        {
            return NotFound();
        }

        return pokemon;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Pokemon newPokemon)
    {
        await _service.CreatePokemonAsync(newPokemon);

        return CreatedAtAction(nameof(Get), new { id = newPokemon.Id }, newPokemon);
    }

    //[HttpPut("{id:length(24)}")]
    //public async Task<IActionResult> Update(string id, Pokemon updatedPokemon)
    //{
    //    var pokemon = await _pokemonService.GetAsync(id);

    //    if (pokemon is null)
    //    {
    //        return NotFound();
    //    }

    //    updatedPokemon.Id = pokemon.Id;

    //    await _pokemonService.UpdateAsync(id, updatedPokemon);

    //    return NoContent();
    //}

    //[HttpDelete("{id:length(24)}")]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    var pokemon = await _pokemonService.GetAsync(id);

    //    if (pokemon is null)
    //    {
    //        return NotFound();
    //    }

    //    await _pokemonService.RemoveAsync(id);

    //    return NoContent();
    //}
}