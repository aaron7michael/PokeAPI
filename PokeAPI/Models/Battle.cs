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
        public required Pokemon PlayerPokemon { get; set; }
        public required Pokemon OpponentPokemon { get; set; }
        public bool IsFinished { get; set; } = false;
        public bool IsPlayerVictorious { get; set; }
        
        public Battle()
        {
            IsFinished = false;
            IsPlayerVictorious = false;
        }
    }
}
