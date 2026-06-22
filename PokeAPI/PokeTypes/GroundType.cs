namespace PokeAPI.Models
{
    internal sealed class GroundType : PokeType
    {
        public GroundType()
        {
            Name = "Ground";
            Resistances = [ "Poison", "Rock" ];
            Weaknesses = [ "Water", "Grass", "Ice" ];
            NoEffect = [ "Electric" ];
        }
    }
}