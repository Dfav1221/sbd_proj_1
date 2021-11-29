namespace DataManagement.Interfaces;

public interface IArrayReader
{
    public int SectionSize { get; set; }
    public Record ReadNextRecord(Stream fileStream);
    public Record ReadRecord(Stream fileStream, int arrayOffset);

    public void Reset();
    bool CheckIfEmpty(Stream fileStream);
    public int GetOffsetSize();
}