namespace DataManagement.Interfaces;

public interface IDiscReader
{
    public Record ReadNextRecord();
    public Record ReadRecord(string arrayPath, int arrayOffset);
}