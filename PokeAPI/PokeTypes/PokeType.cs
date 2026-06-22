using MongoDB.Bson.Serialization;
using PokeAPI.PokeTypes;

namespace PokeAPI.Models
{
    public abstract class PokeType
    {
        public string Name { get; set; }
        public string[] Resistances { get; set; }
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
        public static PokeType GetPokeTypeFromName(string name) =>
        name switch
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
            _ => throw new NotSupportedException($"Unsupported type: {name}"),
        };
    }
    
    public class PokeTypeSerializer : IBsonSerializer<PokeType>
    {
        public Type ValueType => typeof(PokeType);

        public PokeType Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return PokeType.GetPokeTypeFromName(context.Reader.ReadString());
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PokeType value)
        {
            context.Writer.WriteStartDocument();
            context.Writer.WriteName("Name");
            context.Writer.WriteString(value.Name);

            context.Writer.WriteName("Resistances");
            context.Writer.WriteStartArray();
            foreach (string resistance in value.Resistances)
            {
                context.Writer.WriteString(resistance);
            }
            context.Writer.WriteEndArray();

            context.Writer.WriteName("Weaknesses");
            context.Writer.WriteStartArray();
            foreach (string weakness in value.Weaknesses)
            {
                context.Writer.WriteString(weakness);
            }
            context.Writer.WriteEndArray();

            context.Writer.WriteName("NoEffect");
            context.Writer.WriteStartArray();
            foreach (string noEffect in value.NoEffect)
            {
                context.Writer.WriteString(noEffect);
            }
            context.Writer.WriteEndArray();

            context.Writer.WriteEndDocument();
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
