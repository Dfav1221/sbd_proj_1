namespace DataManagement.Interfaces;

public interface IDiscWriter
{
    void DeleteArraySection(int arrayNumber, int sectionSize);
    void WriteRecordAtEnd(int saveArrayIndex, Record record);
}