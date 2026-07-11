using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Swift;
namespace PokeAPI.Models
{
    
    public class Pokemon : IValidatableObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        [BsonSerializer(typeof(PokeTypeArraySerializer))]
        public PokeType[] Types { get; set; }
        public string Status { get; set; } = "healthy";
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SPAttack { get; set; }
        public int SPDefense { get; set; }
        public int Speed { get; set; }
        public Move[] Moves { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = [];
            if (Types.Length > 2 || Types.Length == 0)
            {
                results.Add(new ValidationResult("A Pokemon must have 1 or 2 types.", [nameof(Types)]));
            }
            if (Moves.Length > 4 || Moves.Length == 0)
            {
                results.Add(new ValidationResult("A Pokemon must have 1 to 4 moves.", [nameof(Moves)]));
            }
            return results;
        }
    }
}
