using System;
using System.Collections.Generic;
using System.Text;

namespace KillerrinToolkit.Core.Games.Randomized
{
    public class Coin : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new Coin Instance { get; } = new Coin();

        public Coin() : base() { }
        public Coin(int seed) : base(seed) { }

        public CoinResult FlipCoin()
        {
            var result = Random.Next(2);
            if (result == 0)
                return CoinResult.Heads;
            return CoinResult.Tails;

        }
        public List<CoinResult> FlipCoins(int numberOfCoins)
        {
            List<CoinResult> flips = new List<CoinResult>();
            for (int i = 0; i < numberOfCoins; i++)
            {
                flips.Add(FlipCoin());
            }

            return flips;
        }

        public bool FlipCoinBool() { return Random.Next(2) == 0; }
        public List<bool> FlipCoinsBool(int numberOfCoins)
        {
            List<bool> flips = new List<bool>();
            for (int i = 0; i < numberOfCoins; i++)
            {
                flips.Add(FlipCoinBool());
            }

            return flips;
        }

        public CoinResult FlipCoinWithSide()
        {
            var result = Random.Next(3);
            if (result == 0)
                return CoinResult.Heads;
            else if (result == 1)
                return CoinResult.Tails;
            else return CoinResult.Side;
        }
    }

    public enum CoinResult
    {
        Heads,
        Tails,
        Side
    }
}
