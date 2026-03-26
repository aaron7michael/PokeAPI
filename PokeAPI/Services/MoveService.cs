using PokeAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PokeAPI.Services
{
    public class MoveService
    {
        private readonly IMongoCollection<Move> _moveCollection;

        public MoveService(
            IOptions<PokeAPIDatabaseSettings> pokeAPIDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
                pokeAPIDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                pokeAPIDatabaseSettings.Value.DatabaseName);

            _moveCollection = mongoDatabase.GetCollection<Move>(
                pokeAPIDatabaseSettings.Value.MoveCollectionName);

        }
        public async Task<List<Move>> GetAsync() =>
        await _moveCollection.Find(_ => true).ToListAsync();

        public async Task<Move?> GetAsync(string id) =>
            await _moveCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Move newMove) =>
            await _moveCollection.InsertOneAsync(newMove);

        public async Task UpdateAsync(string id, Move updatedMove) =>
            await _moveCollection.ReplaceOneAsync(x => x.Id == id, updatedMove);

        public async Task RemoveAsync(string id) =>
            await _moveCollection.DeleteOneAsync(x => x.Id == id);
    }
}
