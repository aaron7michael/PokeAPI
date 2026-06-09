using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeAPI.Models
{
    public class PokemonDTO
    {
        public string Name { get; set; }
        public string[] Types { get; set; }
        public string Status { get; set; } = "healthy";
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SPAttack { get; set; }
        public int SPDefense { get; set; }
        public int Speed { get; set; }
        public string[] Moves { get; set; }

        public PokemonDTO (PokemonDTO pokemonDTO)
        {
            Name = pokemonDTO.Name;
            Types = pokemonDTO.Types;
            Status = pokemonDTO.Status;
            HP = pokemonDTO.HP;
            Attack = pokemonDTO.Attack;
            Defense = pokemonDTO.Defense;
            SPAttack = pokemonDTO.SPAttack;
            SPDefense = pokemonDTO.SPDefense;
            Speed = pokemonDTO.Speed;
            Moves = pokemonDTO.Moves;
        }
        public PokemonDTO(Pokemon pokemon)
        {
            Name = pokemon.Name;
            Types = pokemon.Types;
            HP = pokemon.HP;
            Attack = pokemon.Attack;
            Defense = pokemon.Defense;
            SPAttack = pokemon.SPAttack;
            SPDefense = pokemon.SPDefense;
            Speed = pokemon.Speed;
            Moves = pokemon.Moves.Select(m => m.Name).ToArray();
        }
    }
}
