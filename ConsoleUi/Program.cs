using Application;

namespace ConsoleUi;

public static class Program
{
    //Args[] tables at 0 index should have mode of program
    //      1 - read from files on given path at second argument
    //      2 - generate random records, number of them on second argument and path on third
    //      3 - read records from line, to files at second argument,
    public static void Main(string[] args)
    {
        var arguments = args[0].Split(';');
        string[] files = null;
        if (arguments.Length < 2)
            return;
        switch (arguments[0])
        {
            case "1":
                files = Directory.GetFiles(arguments[1]);
                break;
            case "2":
                App.GenerateRecords(arguments[1], arguments[2]);
                files = Directory.GetFiles(arguments[2]);
                break;
            case "3":
                App.ReadRecordsFromUser(arguments[1]);
                break;
        }
        
        

        
        Console.WriteLine("Records before sort");
        App.PrintAllRecords(files.ToList());
        //app.Sort();
        //Console.WriteLine("Records after sort");
        //app.PrintAllRecords();
    }
}

