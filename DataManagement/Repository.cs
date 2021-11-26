using System.IO;
using DataManagement.Array;
using DataManagement.Disc;
using DataManagement.Interfaces;

namespace DataManagement;

public class Repository : IRepository
{
    private readonly List<string> _filePaths;
    private readonly IDiscWriter _discWriter;
    private readonly IDiscReader _discReader;

    public Repository(List<string> filePaths)
    {
        _filePaths = filePaths;
        _discReader = new DiscReader(filePaths);
        _discWriter = new DiscWriter();
    }

    public Record ReadNextRecord()
    {
        return _discReader.ReadNextRecord();
    }

    public Record ReadRecord(string arrayPath, int arrayOffset)
    {
        return _discReader.ReadRecord(arrayPath, arrayOffset);
    }


    public void WriteRecord(Record record, string arrayPath, int arrayOffset)
    {
        _discWriter.WriteRecord(record, arrayPath, arrayOffset);
    }
}