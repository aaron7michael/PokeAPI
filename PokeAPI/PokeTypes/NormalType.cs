namespace PokeAPI.Models
{
    internal class NormalType : PokeType
    {
        public NormalType()
        {
            Name = "Normal";
            Resistances = [];
            Weaknesses = ["Fighting"];
            NoEffect = ["Ghost"];
        }
    }
}