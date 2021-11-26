using DataManagement.Array;
using DataManagement.Interfaces;

namespace DataManagement.Disc;

public class DiscReader : IDiscReader
{
    private int _currentArray;
    private readonly List<IArrayReader> _arrays;
    private readonly List<string> _filePaths;
    private Stream _stream;
    public DiscReader(List<string> filePaths)
    {
        _filePaths = filePaths;
        _arrays = new List<IArrayReader>();
        filePaths.ForEach(file => _arrays.Add(new ArrayReader(file)));
        _currentArray = 0;
        _stream = File.OpenRead(_filePaths[_currentArray]);
    }

    public Record ReadNextRecord()
    {
        var record = _arrays[_currentArray].ReadNextRecord(_stream);
        if (!record.Eof) return record;
        
        _arrays[_currentArray].Reset();
        _currentArray++;
        _stream.Dispose();
        
        if (_currentArray >= _arrays.Count)
        {
  
            return new Record
            {
                ArrayPath = "",
                RecordOffset = 0,
                Mean = 0,
                RecordSize = 0,
                Numbers = null,
                Eof = true
            };
        }
        
        _stream = File.OpenRead(_filePaths[_currentArray]);
        return ReadNextRecord();
    }

    public Record ReadRecord(string arrayPath, int arrayOffset)
    {
        var filePathIndex = _filePaths.IndexOf(arrayPath);
        using var stream = File.OpenRead(arrayPath);
        return _arrays[filePathIndex].ReadRecord(stream, arrayOffset);
    }
}