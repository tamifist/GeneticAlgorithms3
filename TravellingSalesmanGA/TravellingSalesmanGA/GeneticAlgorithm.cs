using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace TravellingSalesmanGA
{
    public class GeneticAlgorithm
    {
        private readonly int populationSize;
        private readonly int chromosomeLength;
        private readonly double crossoverRate;
        private readonly double mutationRate;
        private readonly int elitismCount;
        private readonly int tournamentSize;
        private readonly int maxGenerations;
        private readonly City[] cities;

        public GeneticAlgorithm(int populationSize, double crossoverRate,
            double mutationRate, int elitismCount, int tournamentSize, int maxGenerations, City[] cities)
        {
            this.populationSize = populationSize;
            this.crossoverRate = crossoverRate;
            this.mutationRate = mutationRate;
            this.elitismCount = elitismCount;
            this.tournamentSize = tournamentSize;
            this.maxGenerations = maxGenerations;
            this.cities = cities;
        }

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Population population = new Population(populationSize, cities.Length);

            EvalPopulation(population);

            int currentGeneration = 1;
            Individual fittest;
            while (!IsTerminationConditionMet(currentGeneration))
            {
                population.Individuals = population.OrderByFitness();
                fittest = population.Individuals[0];
                Console.WriteLine("G{0} Best distance: {1}", currentGeneration, fittest.Fitness);

                population = CrossoverPopulation(population);
                population = MutatePopulation(population);

                EvalPopulation(population);

                currentGeneration++;
            }

            fittest = population.GetFittest(0);
            Console.WriteLine("Stopped after {0} generations. Best distance: {1}. Total time: {2}", maxGenerations, fittest.Fitness, stopwatch.ElapsedMilliseconds);
        }

        private Population MutatePopulation(Population population)
        {
            Population newPopulation = new Population(populationSize);

            for (int individualIndex = 0; individualIndex < populationSize; individualIndex++)
            {
                Individual individual = population.Individuals[individualIndex];

                if (individualIndex >= elitismCount)
                {
                    for (int geneIndex = 0; geneIndex < population.Individuals[individualIndex].Chromosome.Length; geneIndex++)
                    {
                        if (Utils.RandomDouble() < mutationRate)
                        {
                            int swapGeneIndex = Utils.RandomInt(0, individual.Chromosome.Length);
                            byte temp = individual.Chromosome[geneIndex];
                            individual.Chromosome[geneIndex] = individual.Chromosome[swapGeneIndex];
                            individual.Chromosome[swapGeneIndex] = temp;
                        }
                    }
                }

                newPopulation.Individuals[individualIndex] = individual;
            }

            return newPopulation;
        }

        private Population CrossoverPopulation(Population population)
        {
            Population nextGeneration = new Population(population.Individuals.Length);
            for (int individualIndex = 0; individualIndex < population.Individuals.Length; individualIndex++)
            {
                Individual parent1 = population.Individuals[individualIndex];

                if (Utils.RandomDouble() < crossoverRate && individualIndex >= elitismCount)
                {
                    Individual parent2 = SelectParent(population);
                    int crossoverPoint1 = Utils.RandomInt(0, parent1.Chromosome.Length);
                    int crossoverPoint2 = Utils.RandomInt(0, parent1.Chromosome.Length);
                    int crossoverStart = Math.Min(crossoverPoint1, crossoverPoint2);
                    int crossoverEnd = Math.Max(crossoverPoint1, crossoverPoint2);
                    nextGeneration.Individuals[individualIndex] = CrossoverTwoIndividuals(parent1, parent2, crossoverStart, crossoverEnd);
                }
                else
                {
                    nextGeneration.Individuals[individualIndex] = parent1;
                }
            }
            return nextGeneration;
        }
        
        private Individual CrossoverTwoIndividuals(Individual parent1, Individual parent2, int crossoverStart, int crossoverEnd)
        {
            Individual offspring = new Individual(parent1.Chromosome.Length, byte.MaxValue);

            // Loop and add the sub tour from parent1 to our child
            for (int i = crossoverStart; i < crossoverEnd; i++)
            {
                offspring.Chromosome[i] = parent1.Chromosome[i];
            }

            // Loop through parent2's city tour
            for (int i = 0; i < parent2.Chromosome.Length; i++)
            {
                int parent2Gene = i + crossoverEnd;
                if (parent2Gene >= parent2.Chromosome.Length)
                {
                    parent2Gene -= parent2.Chromosome.Length;
                }

                // If offspring doesn't have the city add it
                if (!offspring.Chromosome.Contains(parent2.Chromosome[parent2Gene]))
                {
                    // Loop to find a spare position in the child's tour
                    for (int ii = 0; ii < offspring.Chromosome.Length; ii++)
                    {
                        // Spare position found, add city
                        if (offspring.Chromosome[ii] == byte.MaxValue)
                        {
                            offspring.Chromosome[ii] = parent2.Chromosome[parent2Gene];
                            break;
                        }
                    }
                }
            }
            
            return offspring;
        }

        private Individual SelectParent(Population population)
        {
            Population tournament = new Population(population.Shuffle().Take(tournamentSize).ToArray());
            for (int i = 0; i < tournament.Individuals.Length; i++)
            {
                tournament.Individuals[i] = population.Individuals[i];
            }

            return tournament.GetFittest(0);
        }

        private bool IsTerminationConditionMet(int currentGeneration)
        {
            return currentGeneration > maxGenerations;
        }

        private void EvalPopulation(Population population)
        {
            Parallel.ForEach(population.Individuals, individual =>
            {
                CalcFitness(individual);
            });

            population.PopulationFitness = 0;
            foreach (Individual individual in population.Individuals)
            {
                population.PopulationFitness += individual.Fitness;
            }
        }

        private double CalcFitness(Individual individual)
        {
            Route route = new Route(individual, cities);
            individual.Fitness = route.GetTotalDistance();
            return individual.Fitness;
        }
    }
}