using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PokeAPI.Models
{
    public class Move
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public required string Name { get; set; }
        [BsonSerializer(typeof(PokeTypeSerializer))]
        public PokeType Type { get; set; }
        public string StatusEffect { get; set; } = null!;
        public int? StatusChance { get; set; } = null!;
        public required int Attack { get; set; }
        public required int Accuracy { get; set; }
        public required int PP { get; set; }
        public required bool isSpecialAttack { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string[] allowedStatuses = { "burn", "freeze", "paralyze", "poison", "sleep" };
            List<ValidationResult> results = [];
            if (!PokeType.IsValidType(Type))
            {
                results.Add(new ValidationResult($"Invalid type: {Type}", [nameof(Type)]));
            }
            if (StatusEffect != null && !allowedStatuses.Contains(StatusEffect))
            {
                results.Add(new ValidationResult($"Invalid status effect: {StatusEffect}", [nameof(StatusEffect)]));
            }
            return results;
        }
    }
    
}
