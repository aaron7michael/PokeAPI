using Microsoft.AspNetCore.Mvc;
using PokeAPI.DTOs;
using PokeAPI.Models;
using PokeAPI.Services;
using System.ComponentModel.DataAnnotations;

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

    [HttpGet("{name}")]
    public async Task<ActionResult<Pokemon>> Get(string name)
    {
        var pokemon = await _service.GetPokemonAsync(name);

        if (pokemon is null)
        {
            return NotFound();
        }

        return Ok(pokemon);
    }

    [HttpPost]
    public async Task<ActionResult<Pokemon>> Post(PokemonDTO pokemonDTO)
    {
        // check moves exist and throw error if they don't
        Pokemon newPokemon = ModelDTOConverter.PokemonFromPokemonDTO(pokemonDTO);
        for (int i = 0; i < pokemonDTO.Moves.Length; i++)
        {
            Move? existingMove = await _service.GetMoveByNameAsync(pokemonDTO.Moves[i]);
            if (existingMove == null)
            {
                throw new ArgumentException($"Move {pokemonDTO.Moves[i]} does not exist. Please create the move before adding it to a Pokemon.");
            }
            else
            {
                newPokemon.Moves[i] = existingMove;
            }
        }
        List<ValidationResult>? validationResults = ValidatePokemon(newPokemon);
        if(validationResults != null)
        {
            return BadRequest(validationResults);
        }
        await _service.CreatePokemonAsync(newPokemon);
        return CreatedAtAction(nameof(Get), new { name = newPokemon.Name }, newPokemon);
    }

    [HttpPatch("{name}")]
    public async Task<IActionResult> Update(string name, PokemonDTO pokemonDTO)
    {
        Pokemon pokemon = await _service.GetPokemonAsync(name);

        if (pokemon == null)
        {
            return NotFound();
        }
        Pokemon updatedPokemon = ModelDTOConverter.PokemonFromPokemonDTO(pokemonDTO);
        updatedPokemon.Id = pokemon.Id;

        List<ValidationResult>? validationResults = ValidatePokemon(updatedPokemon);
        if (validationResults != null)
        {
            return BadRequest(validationResults);
        }
        await _service.UpdatePokemonAsync(pokemon.Id, updatedPokemon);

        return Ok(updatedPokemon);
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        Pokemon pokemon = await _service.GetPokemonAsync(name);

        if (pokemon == null)
        {
            return NotFound();
        }

        await _service.RemovePokemonAsync(pokemon.Id);

        return NoContent();
    }

    private List<ValidationResult>? ValidatePokemon(Pokemon pokemon)
    {
        var ctx = new ValidationContext(pokemon);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(pokemon, ctx, results, validateAllProperties: true))
            return results;
        return null;
    }
}