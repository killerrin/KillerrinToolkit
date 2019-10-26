using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Core.Games.Randomized
{
    public class Magic8Ball : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new Magic8Ball Instance { get; } = new Magic8Ball();

        #region Replies
        public List<string> Negatives = new List<string> {
            "Very doubtful", 
            "Outlook not so good",
            "My sources say no",
            "My reply is no",
            "Don't count on it",
        };

        public List<string> Neutral = new List<string> {
            "Concentrate and ask again", 
            "Cannot predict now",
            "Better not tell you now",
            "Ask again later",
            "Reply hazy try again"
        };

        public List<string> Positives = new List<string> {
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

        /// <summary>
        /// Generates a Random Negative Response
        /// </summary>
        /// <returns>The response</returns>
        public string RandomNegative()
        {
            return Negatives[Random.Next(0, Negatives.Count)];
        }

        /// <summary>
        /// Generates a Random Neutral Response
        /// </summary>
        /// <returns>The response</returns>
        public string RandomNeutral()
        {
            return Neutral[Random.Next(0, Neutral.Count)];
        }

        /// <summary>
        /// Generates a Random Positive Response
        /// </summary>
        /// <returns>The response</returns>
        public string RandomPositive()
        {
            return Positives[Random.Next(0, Positives.Count)];
        }

        /// <summary>
        /// Generates a Random Response
        /// </summary>
        /// <returns>The response</returns>
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
