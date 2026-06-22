namespace PokeAPI.Models
{
    internal sealed class SteelType : PokeType
    {
        public SteelType()
        {
            Name = "Steel";
            Resistances = [ "Normal", "Flying", "Rock", "Ice", "Bug", "Steel", "Grass", "Psychic", "Dragon", "Fairy" ];
            Weaknesses = [ "Fire", "Fighting", "Ground" ];
            NoEffect = [ "Poison" ];
        }
    }
}