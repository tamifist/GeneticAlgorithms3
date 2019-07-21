using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmSum
{
    class SumChromosome : IChromosome<int>
    {
        public IList<int> Genes { get; set; }
        
        public bool IsAnswer
        {
            get
            {
                return Genes.All(x => x == 1);
            }
        }

        public int GetScore()
        {
            return Genes.Sum();
        }

        public void Mutate(int mutationPoint)
        {
            Genes[mutationPoint] = 1 - Genes[mutationPoint];
        }

        public void SetRandomGenes(int count = 0)
        {
            Genes = new List<int>();
            for (int j = 0; j < count; j++)
            {
                Genes.Add(Utils.GetRandomNumber(0, 2));
            }
        }
    }
}