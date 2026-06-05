namespace PokeAPI.Models
{
    public class MoveDTO
    {
        public required string Name { get; set; }
        public string Type { get; set; }
        public string StatusEffect { get; set; } = null!;
        public int? StatusChance { get; set; } = null!;
        public required int Attack { get; set; }
        public required int Accuracy { get; set; }
        public required int PP { get; set; }
        public required bool isSpecialAttack { get; set; }

        public MoveDTO(Move move)
        {
            Name = move.Name;
            Type = move.Type;
            StatusEffect = move.StatusEffect;
            StatusChance = move.StatusChance;
            Attack = move.Attack;
            Accuracy = move.Accuracy;
            PP = move.PP;
            isSpecialAttack = move.isSpecialAttack;
        }
    }
}
