using System;

namespace TravellingSalesmanGA
{
    public class City
    {
        public City(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public double DistanceTo(City city)
        {
            double deltaXSq = Math.Pow(city.X - X, 2);
            double deltaYSq = Math.Pow(city.Y - Y, 2);

            double distance = Math.Sqrt(deltaXSq + deltaYSq);
            return distance;
        }
    }
}