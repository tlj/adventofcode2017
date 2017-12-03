using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using AdventOfCode;

namespace AdventOfCode2017
{
    public class Day03
    {
        private static int WallSize(int level, int num)
        {
            return (level * 2) - 1 - (num == 1 ? 1 : 0);
        }

        private static int SumNeighbours(int x, int y, Map map)
        {
            var neighbours = Map.NeighboursOf(x, y, true);
            var sum = 0;
            foreach (var n in neighbours)
            {
                if (map.Squares.ContainsKey(n))
                {
                    sum += map.Squares[n].Value;
                }
            }

            return sum;
        }

        public static void Output()
        {
            const int input = 368078;

            var positions = new Dictionary<int, int[]>();
            var level = 1;
            var x = 0;
            var y = 0;
            var n = 1;
            var stored = 0;

            var map = new Map();
            map.Add(x, y, n);
            
            positions[n] = new int[2] { x, y };

            while (n < input)
            {
                level++;
                x++;
                n++;
                positions[n] = new int[2] {x, y};
                if (stored < input)
                {
                    stored = SumNeighbours(x, y, map);
                    map.Add(x, y, stored);
                }

                for (var wall = 1; wall <= 4; wall++)
                {
                    for (var i = 0; i < WallSize(level, wall) - 1; i++)
                    {
                        if (wall == 1) y++;
                        if (wall == 2) x--;
                        if (wall == 3) y--;
                        if (wall == 4) x++;
                        n++;
                        positions[n] = new int[2] {x, y};
                        if (stored < input)
                        {
                            stored = SumNeighbours(x, y, map);
                            map.Add(x, y, stored);
                        }
                    }
                }
            }

            Console.WriteLine("Day 3 Part 1: {0}", Math.Abs(positions[input][0]) + Math.Abs(positions[input][1]));
            Console.WriteLine("Day 3 Part 2: {0}", stored);
        }
    }
}