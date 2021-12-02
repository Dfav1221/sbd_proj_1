using System.Text;
using DataManagement.Interfaces;

namespace DataManagement.Array;

public class ArrayWriter : IArrayWriter
{
    private readonly string _filePath;

    public ArrayWriter(string filePath)
    {
        _filePath = filePath;
    }

    public void DeleteFirstSection(FileStream stream,  int sectionSize)
    {
        var array = new byte[stream.Length];
        stream.Read(array);
        var offset = array.Select((b, i) => b == 10 ? i : -1).Where(i => i != -1).ToArray();
        stream.SetLength(0);
        if (offset.Length == 0)
            return;
        array = array.Skip(offset[sectionSize-1] +1).ToArray();
        stream.Write(array, 0, array.Length);
    }

    public void WriteRecordAtEnd(Record record, FileStream writer,bool lastInSeries = false)
    {
        var actualData = new byte[writer.Length];
        writer.Read(actualData, 0, (int)writer.Length);
        writer.SetLength(0);
        var writtenBytesLength = 0;
        var endLine = lastInSeries ? ";\n" : "\n";
        var data = actualData.Concat(Encoding.ASCII.GetBytes(string.Join(" ", record.Numbers)+endLine)).ToArray();
        var blockSize = 5;

        while (data.Length >= writtenBytesLength)
        {
            if (writtenBytesLength + blockSize > data.Length)
                blockSize = data.Length - writtenBytesLength;
            writer.Write(data, writtenBytesLength, blockSize);
            writtenBytesLength += 5;
        }
    }
}