using PokeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PokeAPI.Services
{
    public class PokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(
            IOptions<PokeAPIDatabaseSettings> pokeAPIDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
                pokeAPIDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                pokeAPIDatabaseSettings.Value.DatabaseName);

            _pokemonCollection = mongoDatabase.GetCollection<Pokemon>(
                pokeAPIDatabaseSettings.Value.PokemonCollectionName);

        }
        public async Task<List<Pokemon>> GetAsync() =>
        await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemon?> GetAsync(string id) =>
            await _pokemonCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pokemon newPokemon) =>
            await _pokemonCollection.InsertOneAsync(newPokemon);

        public async Task UpdateAsync(string id, Pokemon updatedPokemon) =>
            await _pokemonCollection.ReplaceOneAsync(x => x.Id == id, updatedPokemon);

        public async Task RemoveAsync(string id) =>
            await _pokemonCollection.DeleteOneAsync(x => x.Id == id);
    }
}
