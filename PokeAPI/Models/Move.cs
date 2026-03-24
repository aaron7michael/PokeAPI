using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeAPI.Models
{
    public class Move
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
