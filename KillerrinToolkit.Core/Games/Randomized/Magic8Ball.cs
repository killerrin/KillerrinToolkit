using System;
using System.Collections.Generic;
using System.Text;

namespace KillerrinToolkit.Core.Games.Randomized
{
    public class Magic8Ball : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new Magic8Ball Instance { get; } = new Magic8Ball();

        #region Replies
        private static List<string> Negatives = new List<string> {
            "Very doubtful", 
            "Outlook not so good",
            "My sources say no",
            "My reply is no",
            "Don't count on it",
        };

        private static List<string> Neutral = new List<string> {
            "Concentrate and ask again", 
            "Cannot predict now",
            "Better not tell you now",
            "Ask again later",
            "Reply hazy try again"
        };

        private static List<string> Positives = new List<string> {
            "All signs point to yes",
            "Yes",
            "Outlook Good",
            "Most Likely",
            "As I see it, yes",
            "You may rely on it",
            "Yes definitely",
            "Without a doubt",
            "It is decidedly so",
            "It is certain"
        };
        #endregion

        public Magic8Ball() : base() { }
        public Magic8Ball(int seed) : base(seed) { }

        public string RandomNegative()
        {
            return Negatives[Random.Next(0, Negatives.Count)];
        }
        public string RandomNeutral()
        {
            return Neutral[Random.Next(0, Neutral.Count)];
        }
        public string RandomPositive()
        {
            return Positives[Random.Next(0, Positives.Count)];
        }
        public string RandomAll()
        {
            int result = Random.Next(0, 3);
            switch (result)
            {
                case 0: return RandomNegative();
                case 1: return RandomNeutral();
                case 2: return RandomPositive();
                default:
                    return "There was an unexpected error. Please try again later";
            }
        }

    }
}
