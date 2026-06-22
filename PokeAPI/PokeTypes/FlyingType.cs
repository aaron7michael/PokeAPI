namespace PokeAPI.Models
{
    internal sealed class FlyingType : PokeType
    {
        public FlyingType() 
        {
            Name = "Flying";
            Resistances = [ "Fighting", "Bug", "Grass" ];
            Weaknesses = [ "Electric", "Ice", "Rock" ];
            NoEffect = [ ];
        }
    }
}