namespace DataManagement.Interfaces;

public interface IArrayReader
{
    public Record ReadNextRecord(Stream fileStream);
    public Record ReadRecord(Stream fileStream, int arrayOffset);

    public void Reset();
}