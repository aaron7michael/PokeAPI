namespace PokeAPI.Models
{
    public class MoveDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string? StatusEffect { get; set; } = null!;
        public int? StatusChance { get; set; } = null!;
        public int Attack { get; set; }
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public bool isSpecialAttack { get; set; }
    }
}
