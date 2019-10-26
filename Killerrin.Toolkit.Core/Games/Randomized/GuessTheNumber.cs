using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Core.Games.Randomized
{
    public class GuessTheNumber : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new GuessTheNumber Instance { get; } = new GuessTheNumber();

        public int Answer { get; protected set; }
        public int LowerRange { get; protected set; }
        public int UpperRange { get; protected set; }

        public GuessTheNumber() : base() { NewGame(1, 10); }
        public GuessTheNumber(int seed) : base(seed) { NewGame(1, 10); }

        public GuessTheNumber(int lower, int upper) : base() { NewGame(lower, upper); }
        public GuessTheNumber(int seed, int lower, int upper) : base(seed) { NewGame(lower, upper); }

        /// <summary>
        /// Creates a new game using the internal range
        /// </summary>
        public void NewGame()
        {
            Answer = Random.Next(LowerRange, UpperRange + 1);
        }

        /// <summary>
        /// Creates a new game within a given range
        /// </summary>
        /// <param name="lower">The lower range (inclusive)</param>
        /// <param name="upper">The upper range (inclusive)</param>
        public void NewGame(int lower, int upper)
        {
            LowerRange = lower;
            UpperRange = upper;
            NewGame();
        }

        /// <summary>
        /// Checks if a given guess is correct
        /// </summary>
        /// <param name="guess">The guess</param>
        /// <returns>Whether the guess was correct</returns>
        public bool IsAnswer(int guess)
        {
            return guess == Answer;
        }

        /// <summary>
        /// Checks if a given answer is correct, and returns a hint
        /// </summary>
        /// <param name="guess">The guess</param>
        /// <returns>Whether the answer was correct, or a hint</returns>
        public GuessTheNumberResult IsAnswerWithHint(int guess)
        {
            if (guess == Answer)
                return GuessTheNumberResult.Answer;
            else if (guess > Answer)
                return GuessTheNumberResult.TooHigh;
            else return GuessTheNumberResult.TooLow;
        }
    }

    public enum GuessTheNumberResult
    {
        Answer,
        TooHigh,
        TooLow
    }
}
