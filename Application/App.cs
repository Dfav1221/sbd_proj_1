using System.Diagnostics.CodeAnalysis;
using System.Text;
using DataManagement;

namespace Application;

public class App
{
    public static void PrintAllRecords(List<string> files)
    {
        var repository = new Repository(files);
        var it = 0;
        while (true)
        {
            var record = repository.ReadNextRecord();
            if(record.Numbers != null && record.Numbers.All(n=>n == 0))
                continue;
            if (record.ArrayPath == "")
                return;
            it++;
            Console.WriteLine($"{it}-h Numbers: {string.Join(" ", record.Numbers)}   Mean: {record.Mean} ");
        }
    }

    public static void Sort(List<string> files,StatisticLogger logger)
    {
        var repository = new Repository(files,logger);
        var saveArrayIndex = files.Count - 1;
        while (saveArrayIndex != -1)
        {
            saveArrayIndex = SortPhase(repository, saveArrayIndex, files.Count);
            logger.SortPhaseCountInc();
        }
    }

    private static Section MergeSections(Section a, Section b)
    {
        var mergedSection = new Section
        {
            Records = new List<Record>(),
            Size = a.Size + b.Size,
            SizeInBytes = a.SizeInBytes + b.SizeInBytes
        };
        while (a.Records.Count > 0 || b.Records.Count > 0)
        {
            if (b.Records.Count == 0)
            {
                mergedSection.Records.Add(a.Records.First());
                a.Records.RemoveAt(0);
                continue;
            }
            if (a.Records.Count == 0)
            {
                mergedSection.Records.Add(b.Records.First());
                b.Records.RemoveAt(0);
                continue;
            }
            var aMean = a.Records.First().Mean;
            var bMean = b.Records.First().Mean;

            if (aMean > bMean)
            {
                mergedSection.Records.Add(b.Records.First());
                b.Records.RemoveAt(0);
            }
            else
            {
                mergedSection.Records.Add(a.Records.First());
                a.Records.RemoveAt(0);
            }
        }

        return mergedSection;
    }

    private static int SortPhase(Repository repository, int saveArrayIndex, int arrayNumber)
    {
        int clearArrayIndex;
        do
        {
            var newSection = new Section
            {
                Records = new List<Record>(),
                Size = 0,
                SizeInBytes = 0
            };
            for (var i = 0; i < arrayNumber; i++)
            {
                if (i == saveArrayIndex)
                    continue;
                if (repository.IsArrayEmpty(i))
                    continue;
                var arraySection = repository.ReadSection(i);
                newSection = MergeSections(newSection, arraySection);
            }
            repository.SetSectionSize(saveArrayIndex, newSection.Size, newSection.SizeInBytes);
            repository.WriteSection(saveArrayIndex, newSection);
            clearArrayIndex = repository.CheckWhichIsEmpty();
        } while (clearArrayIndex == -2);

        return clearArrayIndex;
    }

    public static void GenerateRecords(int numbersOfRecords, List<string> files)
    {
        files.ForEach(f =>
        {
            using (var filestream = File.OpenWrite(f))
            {
                filestream.SetLength(0);
            }
        });
        
        var randomNumberGenerator = new Random();
        var builder = new StringBuilder();
        for (var i = 0; i < numbersOfRecords; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                var number = randomNumberGenerator.Next(1, 10000);
                builder.Append(number).Append(' ');
            }

            builder.Append('\n');
        }

        using var stream = File.OpenWrite(files[^1]);
        var fileString = builder.ToString();
        var fileContext = Encoding.ASCII.GetBytes(fileString);
        stream.Write(fileContext);
        stream.Close();
    }

    public static void ReadRecordsFromUser(List<string> files)
    {
        throw new NotImplementedException();
    }

    public static void PrintStatisticData(StatisticLogger statisticLogger)
    {
        Console.WriteLine($"Reading operations: {statisticLogger.GetReadCount()}");
        Console.WriteLine($"Writing operations: {statisticLogger.GetWriteCount()}");
        Console.WriteLine($"Sorting Phases: {statisticLogger.GetSortPhaseCount()}");
    }
}