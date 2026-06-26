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
        public string Name { get; set; }
        [BsonSerializer(typeof(PokeTypeSerializer))]
        public PokeType Type { get; set; }
        public string? StatusEffect { get; set; } = null!;
        [Range(1, 100)]
        public int? StatusChance { get; set; } = null!;
        public int Attack { get; set; }
        [Range(1, 100)]
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public bool isSpecialAttack { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string[] allowedStatuses = { "burn", "freeze", "paralyze", "poison", "sleep" };
            List<ValidationResult> results = [];
            if (!PokeType.IsValidType(validationContext.Items["Type"].ToString()))
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
