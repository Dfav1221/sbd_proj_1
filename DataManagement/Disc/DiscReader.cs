using Application;
using DataManagement.Array;
using DataManagement.Interfaces;

namespace DataManagement.Disc;

public class DiscReader : IDiscReader
{
    private int _currentArray;
    private readonly List<IArrayReader> _arrays;
    private readonly List<string> _filePaths;
    private Stream _stream;
    public int BlancRecordsCount { get; set; } = 0;

    public DiscReader(List<string> filePaths)
    {
        _filePaths = filePaths;
        _arrays = new List<IArrayReader>();
        filePaths.ForEach(file => _arrays.Add(new ArrayReader(file)));
        _currentArray = 0;
        _stream = File.OpenRead(_filePaths[0]);
    }

    public Record ReadNextRecord()
    {
        var record = _arrays[_currentArray].ReadNextRecord(_stream);
        if (!record.Eof)
        {
            return record;
        }

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

    public Section ReadSection(int fileIndex)
    {
        _stream.Dispose();
        var section = new Section
        {
            Size = _arrays[fileIndex].SectionSize,
            Records = new List<Record>()
        };

        using var stream = File.Open(_filePaths[fileIndex], FileMode.Open, FileAccess.ReadWrite);
        while(true)
        {
            section
                .Records
                .Add(
                    _arrays[fileIndex]
                        .ReadNextRecord(stream)
                );
            if (section.Records[^1].EoS)
                break;
        }

        section.Size = section.Records.Count;
        section.CanReadMore = _arrays[fileIndex].CheckIfEmpty(stream);
        stream.Dispose();
        return section;
    }

    public void ResetArray(int fileIndex)
    {
        _arrays[fileIndex].Reset();
    }

    public bool IsArrayEmpty(int arrayNumber)
    {
        using var stream = File.OpenRead(_filePaths[arrayNumber]);
        return _arrays[arrayNumber].CheckIfEmpty(stream);
    }

    public void SetSectionSize(int arrayIndex, int newSectionSize, int newSectionSizeInBytes)
    {
        if (_arrays[arrayIndex].SectionSize < newSectionSize)
            _arrays[arrayIndex].SectionSize = newSectionSize;
    }

    public int GetArrayOffset(int arrayNumber)
    {
        return _arrays[arrayNumber].GetOffsetSize();
    }
}