namespace TravellingSalesmanGA
{
    public class Route
    {
        private readonly City[] route;

        public Route(Individual individual, City[] cities)
        {
            route = new City[cities.Length];
            for (int geneIndex = 0; geneIndex < individual.Chromosome.Length; geneIndex++)
            {
                route[geneIndex] = cities[individual.Chromosome[geneIndex]];
            }
        }

        public double GetTotalDistance()
        {
            double totalDistance = 0;
            for (int cityIndex = 0; cityIndex < route.Length - 1; cityIndex++)
            {
                totalDistance += route[cityIndex].DistanceTo(route[cityIndex + 1]);
            }

            totalDistance += route[route.Length - 1].DistanceTo(route[0]);
            
            return totalDistance;
        }
    }
}