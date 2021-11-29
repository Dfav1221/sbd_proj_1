using System.Text;
using System.Text.RegularExpressions;
using DataManagement.Extensions;
using DataManagement.Interfaces;

namespace DataManagement.Array;

public class ArrayReader : IArrayReader
{
    private readonly string _fileName;
    private int _fileOffset;
    private int _readBytesCount;
    private string _buffer;
    public int SectionSize { get; set; }

    public ArrayReader(string fileName)
    {
        _fileName = fileName;
        _fileOffset = 0;
        _readBytesCount = 5;
        _buffer = "";
        SectionSize = 1;
    }


    public Record ReadNextRecord(Stream fileStream)
    {
        var notEof = true;
        var buffer = new byte[100000];
        var numbersAsString = "";
        var startingOffset = _fileOffset;
        
        while (!numbersAsString.Contains('\n') && notEof)
        {
            (notEof, numbersAsString) = ReadNextBlock(buffer,fileStream);
        }
        
        if (!notEof)
            return new Record
            {
                ArrayPath = "",
                RecordOffset = 0,
                RecordSize = 0,
                Mean = 0,
                Eof = true,
                Numbers = null
            };
        numbersAsString = string.Concat(numbersAsString
            .Where(c => c == '\n' || char.IsDigit(c) || c == ' ')
        );
        var splitExcessRecord = numbersAsString.Split('\n');
        var splitNumbers = Regex
            .Split(_buffer + splitExcessRecord[0], @"[^0-9]")
            .ToList();
        
        splitNumbers = splitNumbers
            .Where(s => !string.IsNullOrEmpty(s))
            .ToList();

        _buffer = splitExcessRecord[1];
        var numbers = new List<int>();
        
        splitNumbers.ToList()
            .ForEach(n => numbers.Add(int.Parse(n)));
        
        return new Record
        {
            ArrayPath = _fileName,
            RecordOffset = startingOffset,
            RecordSize = 5,
            Mean = numbers.Mean(),
            Numbers = numbers,
            Eof = !notEof
        };
    }

    public Record ReadRecord(Stream fileStream, int arrayOffset)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        _buffer = "";
        _fileOffset = 0;
        _readBytesCount = 5;
    }

    public bool CheckIfEmpty(Stream fileStream)
    {
        var buffer = new byte[10];
        return fileStream.Read(buffer, 0, 10) == 0;
    }

    public int GetOffsetSize()
    {
        return _fileOffset;
    }

    private (bool, string) ReadNextBlock(byte[] buffer, Stream fileStream)
    {
        var notEof = fileStream.Read(buffer, _fileOffset, _readBytesCount) != 0;
        _fileOffset += _readBytesCount;
        var numbersAsString = Encoding.Default.GetString(buffer);
        return (notEof, numbersAsString);
    }
}