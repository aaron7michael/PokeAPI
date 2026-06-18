using PokeAPI.Models;

namespace PokeAPI.PokeTypes
{
    internal class FireType : PokeType
    {
        public FireType()
        {
            Name = "Fire";
            Strengths = ["Grass", "Ice", "Bug", "Steel"];
            Weaknesses = ["Water", "Ground", "Rock"];
            NoEffect = [];
        }
    }
}
