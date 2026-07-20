using PokeAPI.DTOs;
using PokeAPI.Models;

namespace PokeAPI.Services
{
    public static class BattleCalculator
    {
        public static int CalculateDamage(Move playerMove, PokemonDTO playerPokemon, PokemonDTO opponentPokemon)
        {
            // all pokemon are assumed to be level 100
            double levelModifier = (2 * 100 + 10) / 250;

            (int playerAttackStat, int opponentDefenseStat) = playerMove.isSpecialAttack ?
                (playerPokemon.SPAttack, opponentPokemon.SPDefense)
                : (playerPokemon.Attack, opponentPokemon.Defense);

            double statModifier = (playerMove.Attack * playerAttackStat) / opponentDefenseStat;

            List<double> extraModifiers = [];
            
            // Critial hit modifer
            Random critRandom = new ();
            double critModifier = critRandom.Next(100) <= 6 ? 2.0 : 1.0;
            extraModifiers.Add(critModifier);

            // Type modifiers
            foreach(string type in opponentPokemon.Types)
            {
                PokeType oppType = PokeType.GetPokeTypeFromName(type);
                if (oppType.Weaknesses.Contains(playerMove.Type.Name))
                {
                    extraModifiers.Add(2.0);
                }
                else if (oppType.Resistances.Contains(playerMove.Type.Name) || type == playerMove.Type.Name)
                {
                    extraModifiers.Add(0.5);
                }
            }

            // Same-type attack bonus (STAB) modifier
            if (playerPokemon.Types.Contains(playerMove.Type.Name))
            {
                extraModifiers.Add(1.5);
            }

            //int damage = Convert.ToInt32(Math.Floor());
            double calculatedDamage = levelModifier * statModifier + 2;
            foreach (double em in extraModifiers)
            {
                calculatedDamage *= em;
            }

            // Random modifier
            if (calculatedDamage != 1)
            {
                Random rand = new Random ();
                int randomModifer = rand.Next(217, 256);
                calculatedDamage *= randomModifer;
                return Convert.ToInt32(Math.Floor(calculatedDamage) / 255);
            }
            return Convert.ToInt32(Math.Floor(calculatedDamage));
        }

        private static PokemonDTO Burn(PokemonDTO pokemon)
        {
            PokemonDTO newPokemonDTO = ModelDTOConverter.PokemonDTOFromPokemonDTO(pokemon);
            // Burn status effect logic
            // For example, reduce HP by 5% each turn and reduce Attack by 50%
            newPokemonDTO.HP = (int)(newPokemonDTO.HP * 0.95);
            // newPokemonDTO.Attack = (int)(newPokemonDTO.Attack * 0.5);
            return newPokemonDTO;
        }
        public static PokemonDTO Paralyze(PokemonDTO pokemon)
        {
            PokemonDTO newPokemonDTO = ModelDTOConverter.PokemonDTOFromPokemonDTO(pokemon);
            // Paralyze status effect logic
            // For example, reduce Speed by 50% and have a 25% chance to be unable to move each turn
            newPokemonDTO.Speed = (int)(newPokemonDTO.Speed * 0.5);
            Random rand = new Random();
            if (rand.Next(100) < 25)
            {
                // Pokemon is unable to move this turn
                Console.WriteLine($"{pokemon.Name} is paralyzed and can't move!");
            }
            return newPokemonDTO;
        }
        public static void Poison(Pokemon pokemon)
        {
            // Poison status effect logic
            // For example, reduce HP by 5% each turn
            pokemon.HP = (int)(pokemon.HP * 0.95);
        }
        public static void Sleep(Pokemon pokemon)
        {
            // Sleep status effect logic
            // For example, have a 50% chance to wake up each turn
            Random rand = new Random();
            if (rand.Next(100) < 50)
            {
                // Pokemon wakes up
                Console.WriteLine($"{pokemon.Name} woke up!");
            }
            else
            {
                // Pokemon is still asleep
                Console.WriteLine($"{pokemon.Name} is still asleep.");
            }
        }
        public static void Freeze(Pokemon pokemon)
        {
            // Freeze status effect logic
            // For example, have a 20% chance to thaw out each turn
            Random rand = new Random();
            if (rand.Next(100) < 20)
            {
                // Pokemon thaws out
                Console.WriteLine($"{pokemon.Name} thawed out!");
            }
            else
            {
                // Pokemon is still frozen
                Console.WriteLine($"{pokemon.Name} is still frozen.");
            }
        }
    }
}
