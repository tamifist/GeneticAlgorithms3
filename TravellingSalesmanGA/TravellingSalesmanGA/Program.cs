using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace TravellingSalesmanGA
{
    class Program
    {
        static void Main(string[] args)
        {
            City[] cities = new City[100];
            for (int cityIndex = 0; cityIndex < cities.Length; cityIndex++)
            {
                cities[cityIndex] = new City(Utils.RandomInt(0, 100), Utils.RandomInt(0, 100));
            }
            GeneticAlgorithm ga = new GeneticAlgorithm(100, 0.9, 0.001, 2, 5, 1000, cities);
            ga.Run();
        }
    }
}
