using System.Collections.Generic;
using System.Linq;

namespace TravellingSalesmanGA
{
    public class Population
    {
        public Population(int populationSize)
        {
            Individuals = new Individual[populationSize];
            PopulationFitness = 0;
        }

        public Population(int populationSize, int chromosomeLength)
        {
            Individuals = new Individual[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                Individuals[i] = new Individual(chromosomeLength);
            }
        }

        public Population(Individual[] individuals)
        {
            Individuals = individuals;
        }

        public Individual[] Individuals { get; set; }

        public double PopulationFitness { get; set; }
        
        public Individual GetFittest(int offset)
        {
            return OrderByFitness().ElementAt(offset);
        }

        public Individual[] OrderByFitness()
        {
            return Individuals.OrderBy(x => x.Fitness).ToArray();
        }

        public Individual[] Shuffle()
        {
            Individual[] individuals = new List<Individual>(Individuals).ToArray();
            for (int i = Individuals.Length - 1; i > 0; i--)
            {
                int index = Utils.RandomInt(0, i + 1);
                Individual temp = individuals[index];
                individuals[index] = individuals[i];
                individuals[i] = temp;
            }

            return individuals;
        }
    }
}