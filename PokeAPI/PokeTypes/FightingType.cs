namespace PokeAPI.Models
{
    internal sealed class FightingType : PokeType
    {
        public FightingType() 
        {
            Name = "Fighting";
            Resistances = [ "Bug", "Dark", "Rock" ];
            Weaknesses = [ "Flying", "Psychic", " Fairy" ];
            NoEffect = [ ];
        }
    }
}