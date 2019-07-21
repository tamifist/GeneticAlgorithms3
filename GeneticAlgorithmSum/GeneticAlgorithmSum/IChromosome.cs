using System.Collections.Generic;

namespace GeneticAlgorithmSum
{
    interface IChromosome<TGene>
    {
        IList<TGene> Genes { get; set; }

        bool IsAnswer { get; }

        void SetRandomGenes(int count = 0);

        int GetScore();

        void Mutate(int mutationPoint);
    }
}