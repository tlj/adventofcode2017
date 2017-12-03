using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Map
    {
        public class Square
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Value { get; set; }
        }
        
        public Dictionary<string, Square> Squares { get; set; }

        public Map()
        {
            Squares = new Dictionary<string, Square>();
        }

        public void Add(int x, int y, int value)
        {
            Squares.Add(string.Format("{0}x{1}", x, y), new Square { X = x, Y = y, Value = value });
        }
        
        public static List<string> NeighboursOf(int x, int y, bool withDiagonals)
        {
            var n = new List<string>
            {
                string.Format("{0}x{1}", x - 1, y),
                string.Format("{0}x{1}", x + 1, y),
                string.Format("{0}x{1}", x, y - 1),
                string.Format("{0}x{1}", x, y + 1),
            };

            if (!withDiagonals) return n;

            n.Add(string.Format("{0}x{1}", x - 1, y - 1));
            n.Add(string.Format("{0}x{1}", x - 1, y + 1));
            n.Add(string.Format("{0}x{1}", x + 1, y - 1));
            n.Add(string.Format("{0}x{1}", x + 1, y + 1));

            return n;
        }

    }
}