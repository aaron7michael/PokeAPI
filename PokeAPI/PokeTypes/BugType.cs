namespace PokeAPI.Models
{
    internal sealed class BugType : PokeType
    {
        public BugType() 
        {
            Name = "Bug";
            Resistances = ["Grass", "Fighting", "Ground"];
            Weaknesses = ["Fire", "Flying", "Rock"];
            NoEffect = [];
        }
    }
}