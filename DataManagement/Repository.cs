using System.IO;
using Application;
using DataManagement.Array;
using DataManagement.Disc;
using DataManagement.Interfaces;

namespace DataManagement;

public class Repository : IRepository
{
    private readonly List<string> _filePaths;
    private readonly IDiscWriter _discWriter;
    private readonly IDiscReader _discReader;
    private readonly StatisticLogger? _logger;
    

    public Repository(List<string> filePaths, StatisticLogger? logger = null)
    {
        _filePaths = filePaths;
        _logger = logger;
        _discReader = new DiscReader(filePaths);
        _discWriter = new DiscWriter(filePaths);
        
    }

    public Record ReadNextRecord()
    {
        return _discReader.ReadNextRecord();
    }
    
    public Section ReadSection(int arrayNumber)
    {
        var section = _discReader.ReadSection(arrayNumber);
        _discWriter.DeleteArraySection(arrayNumber, section.Size);
        _discReader.ResetArray(arrayNumber);
        _logger?.ReadCountInc();

        return section;
    }

    public int CheckWhichIsEmpty()
    {
        var emptyFilesCount = 0;
        var result = -2;
        for (var i = 0; i < _filePaths.Count; i++)
        {
            if (!_discReader.IsArrayEmpty(i)) continue;
            
            result = i;
            emptyFilesCount++;
        }

        if (emptyFilesCount == _filePaths.Count - 1)
            return -1;
        return result;
    }

    public void WriteSection(int saveArrayIndex,Section section)
    {
        _logger?.WriteCountInc();
        section.Records.ForEach(r=>_discWriter.WriteRecordAtEnd(saveArrayIndex,r));
    }

    public void SetSectionSize(int arrayIndex, int newSectionSize, int newSectionSizeInBytes)
    {
        _discReader.SetSectionSize(arrayIndex, newSectionSize, newSectionSizeInBytes);
    }

    public bool IsArrayEmpty(int arrayNumber)
    {
        return _discReader.IsArrayEmpty(arrayNumber);
    }
}