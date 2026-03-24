namespace PokeAPI.Models
{
    public class PokeAPIDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PokemonCollectionName { get; set; } = null!;
        public string MoveCollectionName { get; set; } = null!;

    }
}
