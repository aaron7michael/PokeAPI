namespace PokeAPI.Models
{
    internal sealed class WaterType : PokeType
    {
        public WaterType()
        {
            Name = "Water";
            Resistances = [ "Fire", "Water", "Ice", "Steel" ];
            Weaknesses = [ "Grass", "Electric" ];
            NoEffect = [ ];
        }
    }
}