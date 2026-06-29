using MongoDB.Bson;
using MongoDB.Bson.IO;
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
            context.Reader.ReadStartDocument();
            return PokeType.GetPokeTypeFromName(context.Reader.ReadString("Name"));
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PokeType value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();
            writer.WriteName("Name");
            writer.WriteString(value.Name);

            writer.WriteName("Resistances");
            writer.WriteStartArray();
            foreach (string resistance in value.Resistances)
            {
                writer.WriteString(resistance);
            }
            writer.WriteEndArray();

            writer.WriteName("Weaknesses");
            writer.WriteStartArray();
            foreach (string weakness in value.Weaknesses)
            {
                writer.WriteString(weakness);
            }
            writer.WriteEndArray();

            writer.WriteName("NoEffect");
            writer .WriteStartArray();
            foreach (string noEffect in value.NoEffect)
            {
                writer.WriteString(noEffect);
            }
            writer.WriteEndArray();

            writer.WriteEndDocument();
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

    public class PokeTypeArraySerializer : IBsonSerializer<PokeType[]>
    {
        public Type ValueType => typeof(PokeType[]);

        public PokeType[] Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;
            reader.ReadStartArray();
            List<PokeType> pokeTypes = [];
            while(reader.ReadBsonType() != BsonType.EndOfDocument)
            {
                string typeName = reader.ReadString();
                pokeTypes.Add(PokeType.GetPokeTypeFromName(typeName));
            }
            reader.ReadEndArray();
            return [.. pokeTypes];
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PokeType[] value)
        {
            var writer = context.Writer;
            writer.WriteStartArray();
            foreach (PokeType pokeType in value)
            {
                writer.WriteStartDocument();
                writer.WriteName("Name");
                writer.WriteString(pokeType.Name);
                writer.WriteName("Resistances");
                writer.WriteStartArray();
                foreach (string resistance in pokeType.Resistances)
                {
                    writer.WriteString(resistance);
                }
                writer.WriteEndArray();

                writer.WriteName("Weaknesses");
                writer.WriteStartArray();
                foreach (string weakness in pokeType.Weaknesses)
                {
                    writer.WriteString(weakness);
                }
                writer.WriteEndArray();

                writer.WriteName("NoEffect");
                writer.WriteStartArray();
                foreach (string noEffect in pokeType.NoEffect)
                {
                    writer.WriteString(noEffect);
                }
                writer.WriteEndArray();
                writer.WriteEndDocument();
            }
            writer.WriteEndArray();
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
