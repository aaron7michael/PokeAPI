using PokeAPI.Models;

namespace PokeAPI
{
    public class StatusEffects
    {
        public (string, string, string, string, string) validStatusEffects = ( "burn", "paralyze", "poison", "sleep", "freeze" );
        public delegate void StatusEffect();
        public void Burn(Pokemon pokemon)
        {
            // Burn status effect logic
            // For example, reduce HP by 5% each turn and reduce Attack by 50%
            pokemon.HP = (int)(pokemon.HP * 0.95);
            // pokemon.Attack = (int)(pokemon.Attack * 0.5);
        }
        public void Paralyze(Pokemon pokemon)
        {
            // Paralyze status effect logic
            // For example, reduce Speed by 50% and have a 25% chance to be unable to move each turn
            // Speed = (int)(Speed * 0.5);
            Random rand = new Random();
            if (rand.Next(100) < 25)
            {
                // Pokemon is unable to move this turn
                Console.WriteLine($"{pokemon.Name} is paralyzed and can't move!");
            }
        }
        public void Poison(Pokemon pokemon)
        {
            // Poison status effect logic
            // For example, reduce HP by 5% each turn
            pokemon.HP = (int)(pokemon. HP * 0.95);
        }
        public void Sleep(Pokemon pokemon)
        {
            // Sleep status effect logic
            // For example, have a 50% chance to wake up each turn
            Random rand = new Random();
            if (rand.Next(100) < 50)
            {
                // Pokemon wakes up
                Console.WriteLine($"{pokemon.Name} woke up!");
                pokemon.ApplyStatusEffect = null; // Clear the status effect
            }
            else
            {
                // Pokemon is still asleep
                Console.WriteLine($"{pokemon.Name} is still asleep.");
            }
        }
        public void Freeze(Pokemon pokemon)
        {
            // Freeze status effect logic
            // For example, have a 20% chance to thaw out each turn
            Random rand = new Random();
            if (rand.Next(100) < 20)
            {
                // Pokemon thaws out
                Console.WriteLine($"{pokemon.Name} thawed out!");
                pokemon.ApplyStatusEffect = null; // Clear the status effect
            }
            else
            {
                // Pokemon is still frozen
                Console.WriteLine($"{pokemon.Name} is still frozen.");
            }
        }
    }
}
