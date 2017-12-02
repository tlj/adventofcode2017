﻿using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Input
    {
        public static List<char> StringToList(string input)
        {            
            var inputArr = new List<char>();
            inputArr.AddRange(input);
            return inputArr;
        }

        public static List<List<int>> StringToListsOfInt(string input, string splitChar)
        {
            var lines = new List<List<int>>();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var l = new List<int>();
                foreach (var c in line.Split(splitChar))
                {
                    l.Add(int.Parse(c));
                }

                lines.Add(l);
            }

            return lines;
        }
    }
}