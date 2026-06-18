using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using PokeAPI.PokeTypes;

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
    }

    public class PokeTypeSerializer : IBsonSerializer<PokeType>
    {
        public Type ValueType => typeof(PokeType);

        public PokeType Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return context.Reader.ReadString() switch
            {
                "Normal" => new NormalType(),
                "Fire" => new FireType(),
                "Water" => new WaterType(),
                "Electric" => new ElectricType(),
                "Grass" => new GrassType(),
                "Ice" => new IceType(),
                "Fighting" => new FightingType(),
                "Poison" => new PoisonType(),
                "Ground" => new GroundType(),
                "Flying" => new FlyingType(),
                "Psychic" => new PsychicType(),
                "Bug" => new BugType(),
                "Rock" => new RockType(),
                "Ghost" => new GhostType(),
                "Dragon" => new DragonType(),
                "Dark" => new DarkType(),
                "Steel" => new SteelType(),
                "Fairy" => new FairyType(),
                _ => throw new NotSupportedException($"Unsupported type: {context.Reader.ReadString()}"),
            };
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PokeType value)
        {
            context.Writer.WriteString(value.Name);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            Serialize(context, args, (PokeType)value);
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
