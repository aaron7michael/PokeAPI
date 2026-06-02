using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static PokeAPI.StatusEffects;

namespace PokeAPI.Models
{
    public class Move
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public required string Name { get; set; }
        public string Type { get; set; }
        public StatusEffect? Status { get; set; } = null!;
        public int? StatusChance { get; set; } = null!;
        public required int Attack { get; set; }
        public required int Accuracy { get; set; }
        public required int PP { get; set; }
        public required bool isSpecialAttack { get; set; }
    }
}
