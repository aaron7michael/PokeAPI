namespace PokeAPI.Models
{
    internal sealed class GhostType : PokeType
    {
        public GhostType() 
        {
            Name = "Ghost";
            Resistances = [ "Poison", "Bug" ];
            Weaknesses = [ "Ghost", "Dark" ];
            NoEffect = [ "Normal", "Fighting" ];
        }
    }
}