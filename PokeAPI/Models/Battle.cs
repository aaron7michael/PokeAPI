using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace PokeAPI.Models
{
    public class Battle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public PokemonDTO PlayerPokemon { get; set; }
        public MoveDTO[] PlayerMoves { get; set; }
        public PokemonDTO OpponentPokemon { get; set; }
        public MoveDTO[] OpponentMoves { get; set; }
        public bool IsFinished { get; set; } = false;
        public bool IsPlayerVictorious { get; set; }
        
        public Battle(Pokemon playerPokemon, Pokemon opponentPokemon)
        {
            PlayerPokemon = new PokemonDTO(playerPokemon);
            OpponentPokemon = new PokemonDTO(opponentPokemon);
            PlayerMoves = [];
            OpponentMoves = [];

            foreach(Move move in playerPokemon.Moves)
            {
                PlayerMoves = PlayerMoves.Append(new MoveDTO(move)).ToArray();
            }

            foreach(Move move in opponentPokemon.Moves)
            {
                OpponentMoves = OpponentMoves.Append(new MoveDTO(move)).ToArray();
            }
        }
    }
}
