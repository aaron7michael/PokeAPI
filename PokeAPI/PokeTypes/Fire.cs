using PokeAPI.Models;

namespace PokeAPI.PokeTypes
{
    public class Fire : PokeType
    {
        public Fire()
        {
            Name = "Fire";
            Strengths = ["Grass", "Ice", "Bug", "Steel"];
            Weaknesses = ["Water", "Ground", "Rock"];
            NoEffect = [];
        }
    }
}
