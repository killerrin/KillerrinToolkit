using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Killerrin.Toolkit.Core.Games.Randomized
{
    public class Dice : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new Dice Instance { get; } = new Dice();

        public Dice() : base() { }
        public Dice(int seed) : base(seed) { }

        /// <summary>
        /// Rolls a dice between two values
        /// </summary>
        /// <param name="min">The minimum value (inclusive)</param>
        /// <param name="max">The maximum value (exclusive)</param>
        /// <returns>The dice value</returns>
        public int RollBetween(int min, int max) { return Random.Next(min, max); }

        /// <summary>
        /// Rolls a dice with a specific number of sides
        /// </summary>
        /// <param name="sidesOnDice">The number of sides on this dice</param>
        /// <returns>The dice value</returns>
        public int Roll(int sidesOnDice) { return Random.Next(1, sidesOnDice + 1); }

        /// <summary>
        /// Rolls multiple dice with a specific number of sides
        /// </summary>
        /// <param name="numberOfDice">The number of dice to roll</param>
        /// <param name="sidesOnDice">The number of sides on this dice</param>
        /// <returns>The total dice value</returns>
        public int Roll(int numberOfDice, int sidesOnDice)
        {
            if (numberOfDice == 0) numberOfDice = 1;

            int totalRoll = 0;
            for (int i = 0; i < numberOfDice; i++)
                totalRoll += Roll(sidesOnDice);
            return totalRoll;
        }

        /// <summary>
        /// Rolls dice using a DnD Dice String
        /// </summary>
        /// <param name="dieString">A dnd formatted dice string</param>
        /// <returns>The total dice value</returns>
        /// <example>Roll("5d20");</example>
        public int Roll(string dieString)
        {
            var rolls = RollMultiple(dieString);
            int sum = 0;
            foreach (var roll in rolls) { sum += roll; }
            return sum;
        }

        /// <summary>
        /// Rolls multiple dice with a specific number of sides
        /// </summary>
        /// <param name="numberOfDice">The number of dice to roll</param>
        /// <param name="sidesOnDice">The number of sides on this dice</param>
        /// <returns>A list of dice values</returns>
        public List<int> RollMultiple(int numberOfDice, int sidesOnDice)
        {
            if (numberOfDice == 0) numberOfDice = 1;

            List<int> rolls = new List<int>();
            for (int i = 0; i < numberOfDice; i++)
            {
                rolls.Add(Roll(sidesOnDice));
            }

            return rolls;
        }

        /// <summary>
        /// Rolls dice using a DnD Dice String
        /// </summary>
        /// <param name="dieString">A dnd formatted dice string</param>
        /// <returns>A list of dice values</returns>
        /// <example>RollMultiple("5d20");</example>
        public List<int> RollMultiple(string dieString)
        {
            Debug.WriteLine($"Rolling: {dieString}");
            string[] dieSplit = dieString.Split('d');
            Debug.WriteLine($"dieSplit | Length: {dieSplit.Length} | num: {dieSplit[0]}, sides: {dieSplit[1]}");

            // Get the total sides on the dice
            if (!int.TryParse(dieSplit[1], out int sidesOnDice))
                return new List<int>();

            // If the number of Dice isn't specified, assume 1
            if (string.IsNullOrWhiteSpace(dieSplit[0]))
                return RollMultiple(1, sidesOnDice);

            // Alternatively, if it is specified and it parses correctly, roll multiple dice
            if (int.TryParse(dieSplit[0], out int numberOfDice))
                return RollMultiple(numberOfDice, sidesOnDice);

            // Lastly, if nothing else happens roll nothing
            return new List<int>();
        }
    }
}
