namespace DataManagement.Interfaces;

public interface IDiscReader
{
    public Record ReadNextRecord();
    public Record ReadRecord(string arrayPath, int arrayOffset);
    public Section ReadSection(int fileIndex);
    public int GetArrayOffset(int arrayNumber);
    public bool IsArrayEmpty(int arrayNumber);
    void SetSectionSize(int arrayIndex, int newSectionSize, int newSectionSizeInBytes);
    public void ResetArray(int fileIndex);
    public int BlancRecordsCount { get; set; }
}