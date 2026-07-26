using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using PokeAPI.DTOs;

namespace PokeAPI.Models
{
    public class Battle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public List<string> Messages { get; set; }
        public PokemonDTO PlayerPokemon { get; set; }
        public MoveDTO[] PlayerMoves { get; set; }
        public PokemonDTO OpponentPokemon { get; set; }
        public MoveDTO[] OpponentMoves { get; set; }
        public bool IsFinished { get; set; } = false;
        public bool IsPlayerVictorious { get; set; }
        
        public Battle(Pokemon playerPokemon, Pokemon opponentPokemon)
        {
            PlayerPokemon = ModelDTOConverter.PokemonDTOFromPokemon(playerPokemon);
            OpponentPokemon = ModelDTOConverter.PokemonDTOFromPokemon(opponentPokemon);
            PlayerMoves = [.. playerPokemon.Moves.Select(ModelDTOConverter.MoveDTOFromMove)];
            OpponentMoves = [.. opponentPokemon.Moves.Select(ModelDTOConverter.MoveDTOFromMove)];
            Messages = [];
        }
    }
}
