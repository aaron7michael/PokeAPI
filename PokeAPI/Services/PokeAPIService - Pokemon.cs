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

        public async Task CreatePokemonAsync(PokemonDTO newPokemonDTO)
        {
            Pokemon newPokemon = ModelDTOConverter.PokemonFromPokemonDTO(newPokemonDTO);
            // check moves exist and throw error if they don't
            for (int i = 0; i < newPokemonDTO.Moves.Length; i++)
            {
                Move? existingMove = await GetMoveByNameAsync(newPokemonDTO.Moves[i]);
                if (existingMove == null)
                {
                    throw new ArgumentException($"Move {newPokemonDTO.Moves[i]} does not exist. Please create the move before adding it to a Pokemon.");
                }
                else
                {
                    newPokemon.Moves[i] = existingMove;
                }
            }
            await _pokemonCollection.InsertOneAsync(newPokemon);
        }
        public async Task UpdateAsync(string id, Pokemon updatedPokemon) =>
            await _pokemonCollection.ReplaceOneAsync(x => x.Id == id, updatedPokemon);

        public async Task RemoveAsync(string id) =>
            await _pokemonCollection.DeleteOneAsync(x => x.Id == id);
    }
}