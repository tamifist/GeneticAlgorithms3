using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmSum
{
    class Population<TChromosome, TGene> where TChromosome : IChromosome<TGene>, new()
    {
        private readonly int _populationSize;
        private readonly int _chromosomeSize;
        private readonly int _gradedRetainCount;
        private readonly int _nonGradedRetainPercent;

        public Population(int populationSize, int chromosomeSize)
        {
            _populationSize = populationSize;
            _chromosomeSize = chromosomeSize;

            _gradedRetainCount = (int)Math.Round(_populationSize * 0.6);
            _nonGradedRetainPercent = populationSize - _gradedRetainCount;

            Chromosomes = new List<TChromosome>();
            for (int i = 0; i < _populationSize; i++)
            {
                TChromosome child = new TChromosome();
                child.SetRandomGenes(chromosomeSize);
                Chromosomes.Add(child);
            }
        }

        public List<TChromosome> Chromosomes { get; private set; }

        public void NextGeneration()
        {
            Selection();

            IList<TChromosome> children = new List<TChromosome>();
            while (children.Count < _populationSize)
            {
                int a = Utils.GetRandomNumber(0, _populationSize);
                int b = Utils.GetRandomNumber(0, _populationSize);
                TChromosome child = Crossover(Chromosomes[a], Chromosomes[b]);
                Mutation(child);
                children.Add(child);
            }

            Chromosomes.AddRange(children);
        }

        public TChromosome GetAnswer()
        {
            return Chromosomes.FirstOrDefault(x => x.IsAnswer);
        }

        public int GetAverageScore()
        {
            return Chromosomes.Sum(x => x.GetScore()) / Chromosomes.Count;
        }

        private void Selection()
        {
            Chromosomes = Chromosomes.OrderByDescending(x => x.GetScore()).ToList();
            List<TChromosome> result = new List<TChromosome>();
            result.AddRange(Chromosomes.Take(_gradedRetainCount));
            for (int i = 0; i < _nonGradedRetainPercent; i++)
            {
                int j = Utils.GetRandomNumber(0, _populationSize);
                result.Add(Chromosomes[j]);
            }
            Chromosomes = result;
        }

        private TChromosome Crossover(TChromosome chromosome1, TChromosome chromosome2)
        {
            int crossoverPoint = Utils.GetRandomNumber(0, _chromosomeSize + 1);
            List<TGene> childGenes = new List<TGene>();
            childGenes.AddRange(chromosome1.Genes.Take(crossoverPoint));
            childGenes.AddRange(chromosome2.Genes.Skip(crossoverPoint));
            TChromosome child = new TChromosome();
            child.Genes = childGenes;
            return child;
        }

        private void Mutation(TChromosome chromosome)
        {
            int mutationPoint = Utils.GetRandomNumber(0, _chromosomeSize);
            chromosome.Mutate(mutationPoint);
        }
    }
}