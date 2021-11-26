using System.Text;
using DataManagement;
using DataManagement.Interfaces;

namespace Application;

public class App
{
    public static void PrintAllRecords(List<string> files)
    {
        var repository = new Repository(files);
        while(true)
        {
            var record = repository.ReadNextRecord();
            if (record.ArrayPath == "")
                return;
            Console.WriteLine(record.Mean);
        }
    }
    
    public static void Sort(List<string> files)
    {
        
    }

    public static void GenerateRecords(string numberOfRecords, string pathToFolder)
    {
        var numberOfRecordsInt = int.Parse(numberOfRecords);
        var files = Directory.GetFiles(pathToFolder);
        var randomNumberGenerator = new Random();
        foreach (var file in files)
        {
            using (var fileStream = File.OpenWrite(file))
            {
                fileStream.SetLength(0);
                fileStream.Close();
            }
            var builder = new StringBuilder();
            for (var i = 0; i < numberOfRecordsInt; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var number = randomNumberGenerator.Next(1, 10000);
                    builder.Append(number).Append(' ');
                }

                builder.Append('\n');
            }
            using var stream = File.OpenWrite(file);
            var fileString = builder.ToString();
            var fileContext = Encoding.ASCII.GetBytes(fileString);
            stream.Write(fileContext);
            stream.Close();
        }
    }

    public static void ReadRecordsFromUser(string pathToFolder)
    {
        throw new NotImplementedException();
    }
}