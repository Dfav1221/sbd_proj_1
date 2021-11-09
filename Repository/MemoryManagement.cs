using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Repository
{
    public class MemoryManagement
    {
        private Counter _counter;
        private int _fileOffset;
        private string _buffer;

        public MemoryManagement(Counter counter)
        {
            _counter = counter;
            _buffer = "";
            _fileOffset = 0;
        }

        public double ReadNextRecord(Stream source)
        {
            var readNextBytes = true;
            byte[] buffer = new byte[10];

            var numbersAsString = _buffer;
            
            while (readNextBytes)
            {
                var readBytes = source.Read(buffer, _fileOffset, buffer.Length);
                numbersAsString += Encoding.Default.GetString(buffer);
                _fileOffset += readBytes;
                
                
                
                if (!numbersAsString.Contains('\n')) continue;
                
                _buffer = numbersAsString[numbersAsString.IndexOf('\n')..];
                readNextBytes = false;
            }

            var numbersSplit = numbersAsString.Split(' ');
            var numbers = numbersSplit.Select(n => int.Parse(n));
            return numbers.GeometricMean();

        }
    }
}