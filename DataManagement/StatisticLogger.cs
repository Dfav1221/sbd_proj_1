namespace Application;

public class StatisticLogger
{
    private int _readCount;
    private int _writeCount;
    private int _sortPhaseCount;

    public StatisticLogger()
    {
        _readCount = 0;
        _writeCount = 0;
        _sortPhaseCount = 0;
    }

    public void ReadCountInc()
    {
        _readCount++;
    }
    public void WriteCountInc()
    {
        _writeCount++;
    }
    public void SortPhaseCountInc()
    {
        _sortPhaseCount++;
    }
    public int GetReadCount()
    {
        return _readCount;
    }
    public int GetWriteCount()
    {
        return _writeCount;
    }
    public int GetSortPhaseCount()
    {
        return _sortPhaseCount;
    }
}