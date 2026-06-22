namespace PokeAPI.Models
{
    internal sealed class ElectricType : PokeType
    {
        public ElectricType() 
        {
            Name = "Electric";
            Resistances = [ "Electric", "Flying", "Steel" ];
            Weaknesses = [ "Ground" ];
            NoEffect = [ ];
        }
    }
}