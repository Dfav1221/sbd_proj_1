using Application;
using DataManagement.Array;
using DataManagement.Interfaces;

namespace DataManagement.Disc;

public class DiscWriter : IDiscWriter
{
    private readonly List<string> _filePaths;
    private readonly List<IArrayWriter> _arrayWriters;
    public DiscWriter(List<string> filePaths)
    {
        _filePaths = filePaths;
        _arrayWriters = new List<IArrayWriter>();
        filePaths.ForEach(file => _arrayWriters.Add(new ArrayWriter(file)));
    }
    public void WriteRecord(Record record, string arrayPath, int arrayOffset)
    {
        throw new NotImplementedException();
    }

    public void DeleteArraySection(int arrayNumber, int sectionSize)
    {
        using var stream = File.Open(_filePaths[arrayNumber],FileMode.Open,FileAccess.ReadWrite);
        _arrayWriters[arrayNumber].DeleteFirstSection(stream, sectionSize);
        stream.Dispose();
    }

    public void WriteRecordAtEnd(int saveArrayIndex, Record record,bool lastInSeries = false)
    {
        using var writer = File.Open(_filePaths[saveArrayIndex],FileMode.Open,FileAccess.ReadWrite);
        _arrayWriters[saveArrayIndex].WriteRecordAtEnd(record, writer,lastInSeries);
    }


    
}