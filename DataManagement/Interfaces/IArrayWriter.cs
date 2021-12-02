using DataManagement.Array;

namespace DataManagement.Interfaces;

public interface IArrayWriter
{
    void DeleteFirstSection(FileStream stream, int currentArrayOffset);
    void WriteRecordAtEnd(Record record, FileStream writer,bool lastInSeries = false);
}