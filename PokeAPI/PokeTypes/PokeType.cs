using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeAPI.Models
{
    public abstract class PokeType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string[] Strengths { get; set; }
        public string[] Weaknesses { get; set; }
        public string[] NoEffect { get; set; }

        public static string[] ValidTypes =
        {
            "Normal",
            "Fire",
            "Water",
            "Grass",
            "Electric",
            "Ice",
            "Fighting",
            "Poison",
            "Ground",
            "Flying",
            "Psychic",
            "Bug",
            "Rock",
            "Ghost",
            "Dragon",
            "Dark",
            "Steel",
            "Fairy"
        };
        public static bool IsValidType(string type)
        {
            return ValidTypes.Contains(type);
        }
    }
}
