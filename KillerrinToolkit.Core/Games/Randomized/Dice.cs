using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KillerrinToolkit.Core.Games.Randomized
{
    public class Dice : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new Dice Instance { get; } = new Dice();

        public Dice() : base() { }
        public Dice(int seed) : base(seed) { }

        public int RollBetween(int min, int max) { return Random.Next(min, max); }
        public int Roll(int sidesOnDice) { return Random.Next(1, sidesOnDice + 1); }
        public int Roll(int numberOfDice, int sidesOnDice)
        {
            if (numberOfDice == 0) numberOfDice = 1;

            int totalRoll = 0;
            for (int i = 0; i < numberOfDice; i++)
                totalRoll += Roll(sidesOnDice);
            return totalRoll;
        }

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
