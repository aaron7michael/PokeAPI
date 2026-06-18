namespace PokeAPI.Models
{
    internal class BugType : PokeType
    {
        public BugType() 
        {
            Name = "Bug";
            Strengths = ["Grass", "Psychic", "Dark"];
            Weaknesses = ["Fire", "Flying", "Rock"];
            NoEffect = [];
        }
    }
}