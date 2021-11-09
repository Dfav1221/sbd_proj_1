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
            var test = new MemoryManagement(counter);
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                using Stream source = File.OpenRead(file);
                try
                {
                    while (true)
                    {
                        var t = test.ReadNextRecord(source);
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
}