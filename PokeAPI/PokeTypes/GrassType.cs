namespace PokeAPI.Models
{
    internal sealed class GrassType : PokeType
    {
        public GrassType()
        {
            Name = "Grass";
            Resistances = [ "Water", "Electric", "Grass", "Ground" ];
            Weaknesses = [ "Fire", "Ice", "Poison", "Flying", "Bug" ];
            NoEffect = [ ];
        }
    }
}