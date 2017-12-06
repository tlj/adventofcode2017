
using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    public class Day06
    {
        private static int FindIndexOfHighestValue(List<int> banks)
        {
            var high = 0;

            for (var i = 1; i < banks.Count; i++)
            {
                if (banks[i] > banks[high])
                {
                    high = i;
                }
            }

            return high;
        }
        
        public static void Part1()
        {
            var banks = AdventOfCode.Input.StringToListOfInt(Input, "\t");
            var seen = new List<string>();
            var c = 0;

            while (!seen.Contains(string.Join("-", banks)))
            {
                seen.Add(string.Join("-", banks));
                
                var highIndex = FindIndexOfHighestValue(banks);
                var distribute = banks[highIndex];
                banks[highIndex] = 0;
                var di = highIndex;
                
                while (distribute > 0)
                {
                    di++;
                    if (di > banks.Count - 1)
                    {
                        di = 0;
                    }

                    banks[di]++;
                    distribute--;
                }
                
                c++;               
            }

            Input = string.Join("\t", banks);
            Console.WriteLine("Day 6 part 1: {0}", c);
        }

        public static void Part2()
        {
            Part1();
        }
        
        private static string TestInput = "0	2	7	0";
        private static string Input = "4	1	15	12	0	9	9	5	5	8	7	3	14	5	12	3";
    }
}