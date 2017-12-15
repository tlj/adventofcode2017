using System;
using System.Diagnostics;

namespace AdventOfCode2017
{
    public class Program
    {
        static void Main(string[] args)
        {
            Day13.Part1();
            var sw = new Stopwatch();
            sw.Start();
            Day13.Part2();
            sw.Stop();
            Console.WriteLine("Part2 done in {0}ms", sw.ElapsedMilliseconds);
        }
    }
}
