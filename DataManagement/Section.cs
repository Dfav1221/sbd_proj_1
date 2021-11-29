namespace DataManagement;

public record Section
{
    public List<Record> Records { get; set; }
    public int Size { get; set; }
    public int SizeInBytes { get; set; }
    public bool CanReadMore { get; set; }
}