using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeAPI.Models
{
    public class Move
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Attack { get; set; }
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public bool isSpecialAttack { get; set; }
    }
}
