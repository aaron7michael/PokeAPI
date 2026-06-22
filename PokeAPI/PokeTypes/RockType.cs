namespace PokeAPI.Models
{
    internal sealed class RockType : PokeType
    {
        public RockType()
        {
            Name = "Rock";
            Resistances = [ "Fire", "Normal", "Poison", "Flying" ];
            Weaknesses = [ "Water", "Grass", "Fighting", "Ground", "Steel" ];
            NoEffect = [ ];
        }
    }
}