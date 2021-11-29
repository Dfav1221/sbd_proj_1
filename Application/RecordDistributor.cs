using DataManagement;

namespace Application;

public static class RecordDistributor
{
    private static List<int> FibonacciDistribution(int numberCount)
    {
        var sizes = new List<int> {0, 1, 1};
        while (true)
        {
            sizes.Add(
                sizes
                    .GetRange(0, 3)
                    .Aggregate(((next, sum) => sum += next))
            );
            if (sizes[^1] >= numberCount)
                break;
            sizes.RemoveAt(0);
        }

        return sizes;
    }

    public static void DistributeNumbers(Repository repository, List<string> files, int numberCount)
    {
        var distribution = FibonacciDistribution(numberCount);

        var it = 0;
        for (var j = 0; j < 3; j++)
        {
            for (var i = 0; i < distribution[0]; i++)
            {
                var section = repository.ReadSection(3);
                repository.WriteSection(j, section);
                it++;
                if (it == numberCount)
                    break;
            }
            distribution.RemoveAt(0);
        }

        var clearSection = new Section
        {
            Records = new List<Record>
            {
                new()
                {
                    ArrayPath = files[2],
                    Numbers = new List<int> {0, 0, 0, 0, 0},
                    RecordSize = 0
                }
            },
            Size = 1
        };
        while (it < distribution[0])
        {
            repository.WriteSection(2,clearSection);
            it++;
        }
    }
}