using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AdventOfCode;

namespace AdventOfCode2017
{
    public class Day10
    {
        private static int Get(int[] list, int currentPosition, int length)
        {
            if (list.Length - currentPosition >= length)
            {
                return 0;
            }

            return length - (list.Length - currentPosition);
        }
        
        public static void Part1()
        {
            var inputData = File.ReadAllText("Inputs/Day10.txt");
            var input = Input.StringToListOfInt(inputData, ",");
            
            //var input = new List<int> {3,4,1,5};
            
            var currentPosition = 0;
            var skipSize = 0;

            var list = new int[256];
            for (var i = 0; i < 256; i++)
            {
                list[i] = i;
            }

            foreach (var seq in input)
            {
                var len = 0;
                var remainder = 0;
                if (list.Length - currentPosition >= seq)
                {
                    len = seq;
                }
                else
                {
                    len = list.Length - currentPosition;
                    remainder = seq - (list.Length - currentPosition);
                }

                var section = new List<int>();
                for (var i = currentPosition; i < currentPosition + len; i++)
                {
                    section.Add(list[i]);
                }
                for (var i = 0; i < remainder; i++)
                {
                    section.Add(list[i]);
                }
                section.Reverse();

                var rc = 0;
                for (var i = currentPosition; i < currentPosition + len; i++)
                {
                    list[i] = section[rc];
                    rc++;
                }

                for (var i = 0; i < remainder; i++)
                {
                    list[i] = section[rc];
                    rc++;
                }

                currentPosition += seq + skipSize;
                if (currentPosition > list.Length - 1)
                {
                    currentPosition -= list.Length;
                }
                skipSize++;
            }
            Console.WriteLine("Day 10 part 1 {0}", list[0] * list[1]);
        }
        
        public static void Part2()
        {
            var inputData = File.ReadAllText("Inputs/Day10.txt");
            var input = new List<int>();
            foreach (var i in inputData)
            {
                input.Add(i);
            }
            input.AddRange(new List<int> { 17, 31, 73, 47, 23 });
            
            var currentPosition = 0;
            var skipSize = 0;

            var list = new int[256];
            for (var i = 0; i < 256; i++)
            {
                list[i] = i;
            }

            for (var runs = 0; runs < 64; runs++)
            {
                foreach (var seq in input)
                {
                    var len = 0;
                    var remainder = 0;
                    if (list.Length - currentPosition >= seq)
                    {
                        len = seq;
                    }
                    else
                    {
                        len = list.Length - currentPosition;
                        remainder = seq - (list.Length - currentPosition);
                    }

                    var section = new List<int>();
                    for (var i = currentPosition; i < currentPosition + len; i++)
                    {
                        section.Add(list[i]);
                    }

                    for (var i = 0; i < remainder; i++)
                    {
                        section.Add(list[i]);
                    }
                    section.Reverse();

                    var rc = 0;
                    for (var i = currentPosition; i < currentPosition + len; i++)
                    {
                        list[i] = section[rc];
                        rc++;
                    }

                    for (var i = 0; i < remainder; i++)
                    {
                        list[i] = section[rc];
                        rc++;
                    }

                    currentPosition += seq + skipSize;
                    while (currentPosition > list.Length - 1)
                    {
                        currentPosition -= list.Length;
                    }

                    skipSize++;
                }
            }

            var denseHash = new List<int>();
            var pos = 0;
            for (var i = 0; i < 16; i++)
            {
                var num = list[pos];
                pos++;
                for (var j = 1; j < 16; j++)
                {
                    num = num ^ list[pos];
                    pos++;
                }

                denseHash.Add(num);
            }

            var str = new StringBuilder();
            foreach (var num in denseHash)
            {
                str.Append(num.ToString("x2").ToLower());
            }

            Console.WriteLine("Day 10 part 2 {0}", str);
        }

    }
}