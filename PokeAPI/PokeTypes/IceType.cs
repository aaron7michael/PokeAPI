namespace PokeAPI.Models
{
    internal sealed class IceType : PokeType
    {
        public IceType() 
        {
            Name = "Ice";
            Resistances = [ "Ice" ];
            Weaknesses = [ "Fire", "Fighting", "Rock", "Steel" ];
            NoEffect = [ ];
        }
    }
}