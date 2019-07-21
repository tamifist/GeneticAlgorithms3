using System;

namespace GeneticAlgorithmSum
{
    public static class Utils
    {
        private static readonly Random random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            lock (random)
            {
                return random.Next(minValue, maxValue);
            }
        }
    }
}