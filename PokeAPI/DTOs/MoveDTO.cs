using System.ComponentModel.DataAnnotations;

namespace PokeAPI.Models
{
    public class MoveDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string? StatusEffect { get; set; } = null!;
        [Range(1, 100)]
        public int? StatusChance { get; set; } = null!;
        public int Attack { get; set; }
        [Range(1, 100)]
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public bool isSpecialAttack { get; set; }
        [Range(1, 5)]
        public int maxHits { get; set; } = 1;
    }
}
