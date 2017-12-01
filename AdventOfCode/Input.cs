using System;
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
    }
}