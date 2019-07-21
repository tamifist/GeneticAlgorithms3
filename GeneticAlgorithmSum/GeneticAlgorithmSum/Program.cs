using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSum
{
    class Program
    {
        static void Main(string[] args)
        {
            // create the base population
            Population<SumChromosome, int> population = new Population<SumChromosome, int>(10, 10);
            //Print<SumChromosome, int>(population.Chromosomes);

            IList<SumChromosome> answers = new List<SumChromosome>();
            int i = 0;

            // while a solution has not been found
            while (!answers.Any())
            {
                i++;

                // create the next generation
                population.NextGeneration();

                // display the average score of the population (watch it improve)
                Console.WriteLine($"Iteration #{i}. Average score: {population.GetAverageScore()}");

                // check if a solution has been found
                foreach (var chromosome in population.Chromosomes)
                {
                    if (chromosome.IsAnswer)
                    {
                        answers.Add(chromosome);
                    }
                }
            }

            // print the solution
            Console.WriteLine("Answers:");
            Print<SumChromosome, int>(answers);
        }

        private static void Print<TChromosome, TGene>(IList<TChromosome> chromosomes) where TChromosome : IChromosome<TGene>
        {
            foreach (var chromosome in chromosomes)
            {
                Console.WriteLine(string.Join(" ", chromosome.Genes));
            }
        }
    }
}
