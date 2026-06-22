namespace PokeAPI.Models
{
    internal class PsychicType : PokeType
    {
        public PsychicType() 
        {
            Name = "Psychic";
            Resistances = ["Fighting", "Poison"];
            Weaknesses = ["Bug", "Ghost", "Dark"];
            NoEffect = ["Dark"];
        }
    }
}