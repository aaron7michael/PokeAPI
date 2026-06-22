using PokeAPI.Models;

namespace PokeAPI.PokeTypes
{
    internal sealed class FireType : PokeType
    {
        public FireType()
        {
            Name = "Fire";
            Resistances = ["Grass", "Ice", "Bug", "Steel", "Fairy", "Fire"];
            Weaknesses = ["Water", "Ground", "Rock"];
            NoEffect = [];
        }
    }
}
