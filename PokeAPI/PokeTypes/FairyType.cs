namespace PokeAPI.Models
{
    internal sealed class FairyType : PokeType
    {
        public FairyType() 
        {
            Name = "Fairy";
            Resistances = [ "Fighting", "Bug", "Dark" ];
            Weaknesses = [ "Poison", "Steel" ];
            NoEffect = [ "Dragon" ];
        }
    }
}