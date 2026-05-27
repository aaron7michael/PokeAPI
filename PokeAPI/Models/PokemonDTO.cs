using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeAPI.Models
{
    public class PokemonDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string[] Types { get; set; }
        public string Status { get; set; } = "healthy";
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SPAttack { get; set; }
        public int SPDefense { get; set; }
        public int Speed { get; set; }
        public string[] Moves { get; set; }
    }
}
