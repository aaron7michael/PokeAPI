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
            await _moveCollection.Find(x => name.Equals(x.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();

        public async Task<Move> UpdateMoveAsync(string id, Move moveIn) =>
            await _moveCollection.FindOneAndReplaceAsync(x => x.Id == id, moveIn);

        public async Task RemoveMoveAsync(string id) =>
            await _moveCollection.DeleteOneAsync(x => x.Id == id);

    }
}