using System;
using System.IO;
using System.Linq.Expressions;
using System.Net.Mime;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using StructureMap;

namespace Sbd_proj1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];
            var counter = new Counter();
            var test = new MemoryManagement(counter, "");
            using TextReader reader = File.OpenText("input.txt");
            try
            {
                while (true)
                {
                    var t = test.ReadNextRecord(reader);
                    Console.WriteLine(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EOF");
            }
        }
    }
}