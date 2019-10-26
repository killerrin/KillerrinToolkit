using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Core.Games.Randomized
{
    public class Coin : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new Coin Instance { get; } = new Coin();

        public Coin() : base() { }
        public Coin(int seed) : base(seed) { }

        /// <summary>
        /// Flips a coin
        /// </summary>
        /// <returns>Heads or Tails</returns>
        public CoinResult FlipCoin()
        {
            var result = Random.Next(2);
            if (result == 0)
                return CoinResult.Heads;
            return CoinResult.Tails;
        }

        /// <summary>
        /// Flips multiple coins
        /// </summary>
        /// <param name="numberOfCoins">The number of coins you wish to flip</param>
        /// <returns>A list of coin flips</returns>
        public List<CoinResult> FlipCoins(int numberOfCoins)
        {
            List<CoinResult> flips = new List<CoinResult>();
            for (int i = 0; i < numberOfCoins; i++)
            {
                flips.Add(FlipCoin());
            }

            return flips;
        }

        /// <summary>
        /// Flips a coin
        /// </summary>
        /// <returns>A boolean containing the coin flip</returns>
        public bool FlipCoinBool() { return Random.Next(2) == 0; }

        /// <summary>
        /// Flips multiple coins
        /// </summary>
        /// <param name="numberOfCoins">The number of coins you wish to flip</param>
        /// <returns>A list of booleans representing the coin flips</returns>
        public List<bool> FlipCoinsBool(int numberOfCoins)
        {
            List<bool> flips = new List<bool>();
            for (int i = 0; i < numberOfCoins; i++)
            {
                flips.Add(FlipCoinBool());
            }

            return flips;
        }

        /// <summary>
        /// Flips a coin with the potential to land on its side
        /// </summary>
        /// <returns>The Coin Result</returns>
        public CoinResult FlipCoinWithSide()
        {
            var result = Random.Next(3);
            if (result == 0)
                return CoinResult.Heads;
            else if (result == 1)
                return CoinResult.Tails;
            else return CoinResult.Side;
        }

        /// <summary>
        /// Flips multiple coins with the potential for them to land on their side
        /// </summary>
        /// <param name="numberOfCoins">The number of coins you wish to flip</param>
        /// <returns>A list of coin flips</returns>
        public List<CoinResult> FlipCoinsWithSide(int numberOfCoins)
        {
            List<CoinResult> flips = new List<CoinResult>();
            for (int i = 0; i < numberOfCoins; i++)
            {
                flips.Add(FlipCoinWithSide());
            }

            return flips;
        }
    }

    public enum CoinResult
    {
        Heads,
        Tails,
        Side
    }
}
