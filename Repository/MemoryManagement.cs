using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class MemoryManagement
    {
        private Counter _counter;

        private string _filePath;


        public MemoryManagement(Counter counter, string filePath)
        {
            _counter = counter;
            _filePath = filePath;
        }

        public double ReadNextRecord(TextReader reader)
        {
                var line = reader.ReadLine();
                var numbersAsStrings = line.Split(' ').AsEnumerable();
                var numbers = numbersAsStrings.Select(int.Parse);
                var geometricMean = numbers.GeometricMean();
                return geometricMean;
        }
    }
}