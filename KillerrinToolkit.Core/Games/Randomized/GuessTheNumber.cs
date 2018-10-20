using System;
using System.Collections.Generic;
using System.Text;

namespace KillerrinToolkit.Core.Games.Randomized
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

        public void NewGame()
        {
            Answer = Random.Next(LowerRange, UpperRange + 1);
        }
        public void NewGame(int lower, int upper)
        {
            LowerRange = lower;
            UpperRange = upper;
            NewGame();
        }

        public bool IsAnswer(int guess)
        {
            return guess == Answer;
        }
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
