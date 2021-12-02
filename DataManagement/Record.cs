namespace DataManagement;

public record Record
{
    public string ArrayPath { get; set; }
    public int RecordOffset { get; set; }
    public double Mean { get; set; }
    public int RecordSize { get; set; }
    public List<int>? Numbers { get; set; }
    public bool Eof { get; set; } = false;
    public bool EoS { get; set; } = false;
}