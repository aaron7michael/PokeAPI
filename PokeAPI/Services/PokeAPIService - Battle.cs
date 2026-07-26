using PokeAPI.Models;
using MongoDB.Driver;

namespace PokeAPI.Services
{
    public partial class PokeAPIService
    {
        public async Task<List<Battle>> GetBattleAsync() =>
            await _battleCollection.Find(_ => true).ToListAsync();

        public async Task<Battle?> GetBattleAsync(string id) =>
            await _battleCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateBattleAsync(Battle battle) =>
            await _battleCollection.InsertOneAsync(battle);

        public async Task UpdateBattle(Battle battle) =>
            await _battleCollection.ReplaceOneAsync(b => b.Id == battle.Id, battle);

    }
}