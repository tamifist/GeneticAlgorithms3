using System;

namespace TravellingSalesmanGA
{
    public static class Utils
    {
        private static readonly Random Random = new Random();

        public static double RandomDouble()
        {
            lock (Random)
            {
                return Random.NextDouble();
            }
        }

        public static int RandomInt(int minValue, int maxValue)
        {
            lock (Random)
            {
                return Random.Next(minValue, maxValue);
            }
        }
    }
}