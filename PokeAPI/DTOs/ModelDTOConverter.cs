using PokeAPI.Models;
using System.Collections;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace PokeAPI.DTOs
{
    public static class ModelDTOConverter
    {
        public static Pokemon PokemonFromPokemonDTO(PokemonDTO dto)
        {
            return new Pokemon
            {
                Name = dto.Name,
                Types = dto.Types.Select(PokeType.GetPokeTypeFromName).ToArray(),
                Status = dto.Status,
                HP = dto.HP,
                Attack = dto.Attack,
                Defense = dto.Defense,
                SPAttack = dto.SPAttack,
                SPDefense = dto.SPDefense,
                Speed = dto.Speed,
                Moves = new Move[dto.Moves.Length]
            };
        }
        public static PokemonDTO PokemonDTOFromPokemonDTO(PokemonDTO pokemonDTO)
        {
            return new PokemonDTO
            {
                Name = pokemonDTO.Name,
                Types = pokemonDTO.Types,
                Status = pokemonDTO.Status,
                HP = pokemonDTO.HP,
                Attack = pokemonDTO.Attack,
                Defense = pokemonDTO.Defense,
                SPAttack = pokemonDTO.SPAttack,
                SPDefense = pokemonDTO.SPDefense,
                Speed = pokemonDTO.Speed,
                Moves = pokemonDTO.Moves,
            };
        }
        public PokemonDTO(Pokemon pokemon)
        {
            Name = pokemon.Name;
            Types = [.. pokemon.Types.Select(t => t.Name)];
            HP = pokemon.HP;
            Attack = pokemon.Attack;
            Defense = pokemon.Defense;
            SPAttack = pokemon.SPAttack;
            SPDefense = pokemon.SPDefense;
            Speed = pokemon.Speed;
            Moves = pokemon.Moves.Select(m => m.Name).ToArray();
        }
        public static MoveDTO MoveDTOFromMove(Move move)
        {
            return new MoveDTO
            {
                Name = move.Name,
                Type = move.Type.Name,
                StatusEffect = move.StatusEffect,
                StatusChance = move.StatusChance,
                Attack = move.Attack,
                Accuracy = move.Accuracy,
                PP = move.PP,
                isSpecialAttack = move.isSpecialAttack,
            };
        }

        public static Move MoveFromMoveDTO(MoveDTO newMoveDTO)
        {
            return new Move
            {
                Name = newMoveDTO.Name,
                Type = PokeType.GetPokeTypeFromName(newMoveDTO.Type),
                StatusEffect = newMoveDTO.StatusEffect,
                StatusChance = newMoveDTO.StatusChance,
                Attack = newMoveDTO.Attack,
                Accuracy = newMoveDTO.Accuracy,
                PP = newMoveDTO.PP,
                isSpecialAttack = newMoveDTO.isSpecialAttack,
            };
        }
    }
}
