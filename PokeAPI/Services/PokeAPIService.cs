using PokeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PokeAPI.Services
{
    public partial class PokeAPIService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;
        private readonly IMongoCollection<Move> _moveCollection;
        private readonly IMongoCollection<Battle> _battleCollection;
        private readonly (string, string, string, string, string) validStatusEffects = ("burn", "paralyze", "poison", "sleep", "freeze");

        public PokeAPIService(
            IOptions<PokeAPIDatabaseSettings> pokeAPIDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
                pokeAPIDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                pokeAPIDatabaseSettings.Value.DatabaseName);

            _pokemonCollection = mongoDatabase.GetCollection<Pokemon>(
                pokeAPIDatabaseSettings.Value.PokemonCollectionName);

            _moveCollection = mongoDatabase.GetCollection<Move>(
                pokeAPIDatabaseSettings.Value.MoveCollectionName);

            _battleCollection = mongoDatabase.GetCollection<Battle>(
                pokeAPIDatabaseSettings.Value.BattleCollectionName);
        }
    }
}