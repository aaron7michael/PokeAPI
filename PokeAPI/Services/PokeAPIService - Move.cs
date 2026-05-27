using PokeAPI.Models;
using MongoDB.Driver;

namespace PokeAPI.Services
{
    public partial class PokeAPIService
    {
        public async Task CreateMoveAsync(Move move) =>
            await _moveCollection.InsertOneAsync(move);

        public async Task<List<Move>> GetMoveAsync() =>
            await _moveCollection.Find(_ => true).ToListAsync();

        public async Task<Move?> GetMoveAsync(string id) =>
            await _moveCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Move?> GetMoveByNameAsync(string name) =>
            await _moveCollection.Find(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();

    }
}