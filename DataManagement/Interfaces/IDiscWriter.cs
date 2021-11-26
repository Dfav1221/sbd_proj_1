namespace DataManagement.Interfaces;

public interface IDiscWriter
{
    public void WriteRecord(Record record, string arrayPath, int arrayOffset);
}