using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static PokeAPI.StatusEffects;
namespace PokeAPI.Models
{
    
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string[] Types { get; set; }
        public string Status { get; set; } = "healthy";
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SPAttack { get; set; }
        public int SPDefense { get; set; }
        public int Speed { get; set; }
        public Move[] Moves { get; set; }

        public Pokemon(PokemonDTO dto)
        {
            if (dto.Types.Length > 2 || dto.Types.Length == 0)
            {
                throw new ArgumentException("A Pokemon must have 1 or 2 types.");
            }
            if (dto.Moves.Length > 4 || dto.Moves.Length == 0)
            {
                throw new ArgumentException("A Pokemon must have 1 to 4 moves.");
            }

            Name = dto.Name;
            Types = dto.Types;
            Status = dto.Status;
            HP = dto.HP;
            Attack = dto.Attack;
            Defense = dto.Defense;
            SPAttack = dto.SPAttack;
            SPDefense = dto.SPDefense;
            Speed = dto.Speed;
            Moves = new Move[dto.Moves.Length];
        }
    }
}
