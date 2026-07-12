using PokeAPI.Models;
using MongoDB.Driver;
using PokeAPI.DTOs;

namespace PokeAPI.Services
{
    public partial class PokeAPIService
    {
        public async Task<List<Pokemon>> GetPokemonAsync() =>
            await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemon?> GetPokemonAsync(string name) =>
            await _pokemonCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        
        internal async Task<Pokemon?> GetPokemonRandomPokemonAsync()
        {
            var allPokemon = await GetPokemonAsync();
            Random rand = new Random();
            int index = rand.Next(allPokemon.Count);

            return allPokemon.Count > 0 ? allPokemon[index] : null;
        }

        public async Task CreatePokemonAsync(Pokemon newPokemon) =>
            await _pokemonCollection.InsertOneAsync(newPokemon);

        public async Task UpdatePokemonAsync(string id, Pokemon updatedPokemon) =>
            await _pokemonCollection.ReplaceOneAsync(x => x.Id == id, updatedPokemon);

        public async Task RemovePokemonAsync(string id) =>
            await _pokemonCollection.DeleteOneAsync(x => x.Id == id);
    }
}