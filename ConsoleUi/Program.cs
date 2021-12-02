using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Application;
using DataManagement;
using Microsoft.Extensions.Configuration;

namespace ConsoleUi;

public static class Program
{
    //Args[] tables at 0 index should have mode of program
    //      1 - read from files on given path at second argument
    //      2 - generate random records, number of them on second argument and path on third
    //      3 - read records from line, to files at second argument,
                                                                                                                                                                                                                                                                             [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public static void Main(string[] args)
    {
        
        var config = new ConfigurationBuilder()
            .AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json")
            .Build();

        var files = Directory.GetFiles(config["folderPath"]);

        var numberCount = config
            .GetSection("tapeSize")
            .Get<int>();
        
        switch (config["selectedMode"])
        {
            case "files":
                break;
            case "random":
                App.GenerateRecords(numberCount, files.ToList());
                break;
            case "console":
                App.ReadRecordsFromUser(files.ToList());
                break;
        }



        var statisticLogger = new StatisticLogger();
        Console.WriteLine("Records before sort");
        App.PrintAllRecords(files.ToList());
        var blancRecordsCount = RecordDistributor.DistributeNumbers(new Repository(files.ToList()),files.ToList(),numberCount);
        App.Sort(files.ToList(),blancRecordsCount,statisticLogger);
        Console.WriteLine("Records after sort");
        App.PrintAllRecords(files.ToList());
        App.PrintStatisticData(statisticLogger);
    }
}

