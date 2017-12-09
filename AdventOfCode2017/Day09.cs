using System;
using System.IO;

namespace AdventOfCode2017
{
    public class Day09
    {
        public static void Part1()
        {
            var input = File.ReadAllText("Inputs/Day09.txt");
            //var input = "{{<a!>},{<a!>},{<a!>},{<ab>}}";

            var ignore = false;
            var level = 0;
            var score = 0;
            var garbage = false;
            var gc = 0;
            
            foreach (var c in input)
            {
                if (ignore)
                {
                    ignore = false;
                    continue;
                }

                if (garbage && c != '!' && c != '>')
                {
                    gc++;
                    continue;
                }
            
                switch (c)
                {
                        case '{':
                            level++;
                            break;
                        case '}':
                            score += level; 
                            level--;
                            break;
                        case '<':
                            garbage = true;
                            break;
                        case '>':
                            garbage = false;
                            break;
                        case '!':
                            ignore = !ignore;
                            break;
                }
            }

            Console.WriteLine("Day 09 part 1: {0}, {1}", score, gc);
        }
    }
}