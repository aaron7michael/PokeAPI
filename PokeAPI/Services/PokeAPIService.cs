using PokeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PokeAPI.Services
{
    public class PokeAPIService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;
        private readonly IMongoCollection<Move> _moveCollection;
        private readonly IMongoCollection<Battle> _battleCollection;

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
        public async Task<List<Pokemon>> GetPokemonAsync() =>
            await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemon?> GetPokemonAsync(string name) =>
            await _pokemonCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        
        public async Task<List<Move>> GetMoveAsync() =>
            await _moveCollection.Find(_ => true).ToListAsync();
        
        public async Task<Move?> GetMoveAsync(string id) =>
            await _moveCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Move?> GetMoveByNameAsync(string name) =>
            await _moveCollection.Find(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();

        public async Task<List<Battle>> GetBattleAsync() =>
            await _battleCollection.Find(_ => true).ToListAsync();

        public async Task<Battle?> GetBattleAsync(string id) =>
            await _battleCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateBattleAsync(Battle battle) =>
            await _battleCollection.InsertOneAsync(battle);

        internal async Task<Pokemon?> GetPokemonRandomPokemonAsync()
        {
            var allPokemon = await GetPokemonAsync();
            Random rand = new Random();
            int index = rand.Next(allPokemon.Count);

            return allPokemon.Count > 0 ? allPokemon[index] : null;
        }

        public async Task CreatePokemonAsync(Pokemon newPokemon)
        {
            // check moves and create if move doesn't exist
            for(int i = 0; i < newPokemon.Moves.Length; i++)
            {
                Move? existingMove = await GetMoveByNameAsync(newPokemon.Moves[i].Name);
                if (existingMove == null)
                {
                    await _moveCollection.InsertOneAsync(newPokemon.Moves[i]);
                }
                else
                {
                    newPokemon.Moves[i] = existingMove;
                }
            }
            await _pokemonCollection.InsertOneAsync(newPokemon);
        }
            

        //public async Task UpdateAsync(string id, Pokemon updatedPokemon) =>
        //    await _pokemonCollection.ReplaceOneAsync(x => x.Id == id, updatedPokemon);

        //public async Task RemoveAsync(string id) =>
        //    await _pokemonCollection.DeleteOneAsync(x => x.Id == id);
    }
}