namespace PokeAPI.Models
{
    internal sealed class PoisonType : PokeType
    {
        public PoisonType() 
        {
            Name = "Poison";
            Resistances = [ "Grass", "Fighting", "Poison", "Bug", "Fairy" ];
            Weaknesses = [ "Ground", "Psychic" ];
            NoEffect = [ ];
        }
    }
}