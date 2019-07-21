using System;

namespace TravellingSalesmanGA
{
    public class Individual
    {
        public Individual(byte[] chromosome)
        {
            Chromosome = chromosome;
        }

        public Individual(int chromosomeLength)
        {
            Chromosome = new byte[chromosomeLength];
            for (byte gene = 0; gene < chromosomeLength; gene++)
            {
                Chromosome[gene] = gene;
            }
        }

        public Individual(int chromosomeLength, byte initGene)
        {
            Chromosome = new byte[chromosomeLength];
            for (int gene = 0; gene < chromosomeLength; gene++)
            {
                Chromosome[gene] = initGene;
            }
        }

        public byte[] Chromosome { get; set; }

        public double Fitness { get; set; } = int.MaxValue;

        public override string ToString()
        {
            return string.Format("[ {0} ]", string.Join(" ", Chromosome));
        }
    }
}