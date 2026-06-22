namespace PokeAPI.Models
{
    internal sealed class DragonType : PokeType
    {
        public DragonType() 
        {
            Name = "Dragon";
            Resistances = [ "Fire", "Water", "Grass", "Electric" ];
            Weaknesses = [ "Ice", "Fairy", "Dragon" ];
            NoEffect = [ ];
        }
    }
}