using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{    
    public class Day12
    {        
        public static void Part1()
        {
            var input = File.ReadAllText("Inputs/Day12.txt");
            var programs = new Dictionary<int, List<int>>();

            foreach (var l in input.Split(Environment.NewLine))
            {
                var parts = l.Split(" ");
                var program = int.Parse(parts[0]);
                if (!programs.ContainsKey(program))
                {
                    programs[program] = new List<int> { program };
                }

                var connectionsString = l.Substring(l.IndexOf("->") + 2);
                foreach (var connectionChar in connectionsString.Split(","))
                {
                    var cc = connectionChar.Trim();
                    if (cc == "") continue;
                    
                    var connection = int.Parse(cc);
                    if (!programs.ContainsKey(connection))
                    {
                        programs[connection] = new List<int> {program, connection};
                    }
                    if (!programs[program].Contains(connection))
                    {
                        programs[program].Add(connection);
                    }                    
                }
            }

            foreach (var connectionsFor in programs)
            {
                var added = new List<int> {connectionsFor.Key};
                var seen = new List<int>();
                //var connectionsFor = 0;

                while (added.Count > 0)
                {
                    programs[connectionsFor.Key].AddRange(added);
                    added = new List<int>();

                    foreach (var p in programs[connectionsFor.Key])
                    {
                        if (seen.Contains(p) || p == connectionsFor.Key) continue;
                        seen.Add(p);

                        foreach (var c in programs[p])
                        {
                            if (!programs[connectionsFor.Key].Contains(c))
                            {
                                added.Add(c);
                            }
                        }
                    }
                }
            }

            var groups = new List<List<int>>();
            groups.Add(programs[0]);
            
            foreach (var p in programs)
            {
                if (p.Key == 0)
                {
                    continue;
                }

                var intersects = false;
                foreach (var group in groups)
                {
                    if (group.Intersect(p.Value).Any())
                    {
                        intersects = true;
                    }
                }

                if (!intersects)
                {
                    groups.Add(p.Value);
                }
            }

            Console.WriteLine("Day 12, Part 1: {0}", programs[0].Distinct().Count());
            Console.WriteLine("Day 12, Part 2: {0}", groups.Count());
        }
    }
}