namespace DataManagement.Interfaces;

public interface IRepository
{
    public Record ReadNextRecord();
    public Record ReadRecord(string arrayPath, int arrayOffset);
    public void WriteRecord(Record record, string arrayPath, int arrayOffset);
}