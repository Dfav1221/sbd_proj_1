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

    public ArrayReader(string fileName)
    {
        _fileName = fileName;
        _fileOffset = 0;
        _readBytesCount = 5;
        _buffer = "";
    }

    public Record ReadNextRecord(Stream fileStream)
    {
        var notEof = true;
        var lengthOfFile = (int) fileStream.Length;
        var buffer = new byte[512];
        var numbersAsString = "";
        var startingOffset = _fileOffset;
        while (!numbersAsString.Contains('\n') && notEof)
        {
            notEof = fileStream.Read(buffer, _fileOffset, _readBytesCount) != 0;
            _fileOffset += _readBytesCount;
            numbersAsString = Encoding.Default.GetString(buffer);
        }
        
        //numbersAsString = Regex.Match(_buffer + numbersAsString, @"[1-9 \n]+").Value;
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
        var splitExcessRecord = numbersAsString.Split('\n');
        var splitNumbers = Regex
            .Split(_buffer + splitExcessRecord[0], @"[^1-9]").ToList();
        splitNumbers = splitNumbers.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();

        _buffer = splitExcessRecord[1];
        //var splitNumbers = numbersAsString.Split(' ');
        var numbers = new List<int>();
        splitNumbers.ToList().ForEach(n => numbers.Add(int.Parse(n)));
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

    private bool SkipUselessBytes(byte character)
    {
        switch (character)
        {
            case (byte) '\n':
            case (byte) ' ':
                return true;
            default:
                return character is >= (byte) '0' and <= (byte) '9';
        }
        
    }
}