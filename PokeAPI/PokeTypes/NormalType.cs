namespace PokeAPI.Models
{
    internal class NormalType : PokeType
    {
        public NormalType()
        {
            Name = "Normal";
            Strengths = [];
            Weaknesses = ["Fighting"];
            NoEffect = ["Ghost"];
        }
    }
}