namespace PokeAPI.Models
{
    internal sealed class DarkType : PokeType
    {
        public DarkType() 
        {
            Name = "Dark";
            Resistances = [ "Dark", "Ghost" ];
            Weaknesses = [ "Fighting", "Bug", "Fairy" ];
            NoEffect = [ "Psychic" ];
        }
    }
}