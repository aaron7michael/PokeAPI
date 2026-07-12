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
        [Key]
        public string Name { get; set; }
        [BsonSerializer(typeof(PokeTypeArraySerializer))]
        [MaxLength(2, ErrorMessage = "A Pokemon must have 1 or 2 types.")]
        [MinLength(1, ErrorMessage = "A Pokemon must have at least 1 type.")]
        public PokeType[] Types { get; set; }
        public string Status { get; set; } = "healthy";
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SPAttack { get; set; }
        public int SPDefense { get; set; }
        public int Speed { get; set; }
        [MaxLength(4, ErrorMessage = "A Pokemon must have 1 to 4 moves.")]
        public Move[] Moves { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = [];
            foreach (PokeType type in Types)
            {
                if (type == null || !PokeType.IsValidType(type.Name))
                {
                    results.Add(new ValidationResult($"Invalid type: {type}", [nameof(Types)]));
                }
            }
            return results;
        }
    }
}
