using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace PokeAPI.Models
{
    public class Battle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required PokemonDTO PlayerPokemon { get; set; }
        public required MoveDTO[] PlayerMoves { get; set; }
        public required PokemonDTO OpponentPokemon { get; set; }
        public required MoveDTO[] OpponentMoves { get; set; }
        public bool IsFinished { get; set; } = false;
        public bool IsPlayerVictorious { get; set; }
        
        public Battle()
        {
            IsFinished = false;
            IsPlayerVictorious = false;
        }
    }
}
