using DataManagement;

namespace Application;

public static class RecordDistributor
{
    private static List<int> FibonacciDistribution(int numberCount)
    {
        var sizes = new List<int> {0, 1};
        while (true)
        {
            sizes.Add(
                sizes
                    .GetRange(0, 2)
                    .Aggregate(((next, sum) => sum += next))
            );
            if (sizes[^1] >= numberCount)
                break;
            sizes.RemoveAt(0);
        }

        return sizes;
    }

    public static int DistributeNumbers(Repository repository, List<string> files, int numberCount)
    {
        var distribution = FibonacciDistribution(numberCount);
        var it = 0;
        for (var j = 0; j < 2; j++)
        {
            for (var i = 0; i < distribution[0]; i++)
            {
                var section = repository.ReadSection(2);
                repository.WriteSection(j, section);
                it++;
                if (it == numberCount)
                    break;
            }
            distribution.RemoveAt(0);
        }

        return distribution[0] - it;
    }
}