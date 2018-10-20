using System;
using System.Collections.Generic;
using System.Text;

namespace KillerrinToolkit.Core.Games.Randomized
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
    }
}
