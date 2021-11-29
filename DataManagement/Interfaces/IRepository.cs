namespace DataManagement.Interfaces;

public interface IRepository
{
    public Record ReadNextRecord();
    public Section ReadSection(int arrayNumber);
    public int CheckWhichIsEmpty();
    public void WriteSection(int saveArrayIndex, Section section);
    public void SetSectionSize(int arrayIndex, int newSectionSize, int newSectionSizeInBytes);
    public bool IsArrayEmpty(int arrayNumber);
}