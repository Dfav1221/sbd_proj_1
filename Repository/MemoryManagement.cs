using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Repository
{
    public class MemoryManagement
    {
        private Counter _counter;
        private string _buffer;
        private int _FileOffset;

        public MemoryManagement(Counter counter)
        {
            _counter = counter;
            _FileOffset = 0;
            _buffer = "";
        }

        public double ReadNextRecord(Stream source)
        {
            var notEof = true;
            var recordSize = 5;
            var lengthOfFile = (int)source.Length;
            byte[] buffer = new byte[64];
            var numbersAsString = "";
            while (numbersAsString.IndexOf('\n') == -1 && notEof)
            {
                if (_FileOffset + recordSize >= lengthOfFile)
                {
                    notEof = false;
                    recordSize = lengthOfFile - _FileOffset;
                }

                source.Read(buffer, _FileOffset, recordSize);
                numbersAsString = Encoding.Default.GetString(buffer);
                _FileOffset += recordSize;
            }

            numbersAsString = _buffer + numbersAsString;
            numbersAsString = numbersAsString.Remove(numbersAsString.IndexOf('\r'), 1);
            var splitExcessRecord = numbersAsString.Split("\n");
            var nullIndex = splitExcessRecord[1].IndexOf(null!, StringComparison.Ordinal);
            _buffer = splitExcessRecord[1];
            var numbersSplit = splitExcessRecord[0].Split(' ');
            var numbers = new int[numbersSplit.Length];
            for (var it = 0; it < numbersSplit.Length; it++)
            {
                numbers[it] = int.Parse(numbersSplit[it]);
            }
            return numbers.GeometricMean();
        }
    }
}