using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Core.Games.Randomized
{
    public class Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static Randomizer Instance { get; } = new Randomizer();

        public Random Random { get; protected set; }

        public Randomizer()
        {
            Random = new Random();
        }
        public Randomizer(int seed)
        {
            Random = new Random(seed);
        }
        public void ChangeSeed(int seed)
        {
            Random = new Random(seed);
        }
        
        /// <summary>
        /// Randomly selects a number of 0 or 1
        /// </summary>
        /// <returns>A binary result of either 0 or 1</returns>
        public int RandomBinary() { return Random.Next(2); }
        
        /// <summary>
        /// Choses a number between 1 and 100 (both inclusive) then checks to see if it hit the required difficulty
        /// </summary>
        /// <param name="difficulty">The minimum value that has to be hit for a success</param>
        /// <returns>Whether the value fell within the set difficulty</returns>
        public bool DifficultyCheck(int difficulty)
        {
            var value = Random.Next(1, 101);
            return value >= difficulty;
        }
        
        /// <summary>
        /// Chooses a number between 1 and 100 (both inclusive) then checks to see if it falls within the range
        /// </summary>
        /// <param name="minRange">The Inclusive Minimum on the range</param>
        /// <param name="maxRange">The Inclusive Maximum on the range</param>
        /// <returns>Whether the value fell within the range</returns>
        public bool PercentageCheck(int minRange, int maxRange) {
            var value = Random.Next(1, 101);
            return (value >= minRange) && (value <= maxRange);
        }
        
        /// <summary>
        /// Selects a random item out of a list
        /// </summary>
        /// <typeparam name="T">The type of the item to be returned</typeparam>
        /// <param name="items">The list of items that we get a random element from</param>
        /// <returns>A random element out of the list</returns>
        public T RandomElement<T>(IList<T> items)
        {
            return items[Random.Next(0, items.Count)];
        }
    }
}
