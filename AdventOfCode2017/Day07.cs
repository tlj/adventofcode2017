using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    public class Day07
    {
        public static void Part1()
        {
            var left = new List<string>();
            var hasChildren = new List<string>();
            var input = File.ReadAllText("Inputs/Day07.txt");
            foreach (var line in input.Split(Environment.NewLine))
            {
                var firstWord = line.Substring(0, line.IndexOf(" "));
                left.Add(firstWord);
            }

            foreach (var line in input.Split(Environment.NewLine))
            {
                if (line.Contains("->"))
                {
                    var line2 = (string)line.Clone();
                    var children = line2.Substring(line2.IndexOf("->"));
                    
                    foreach (var l in left)
                    {
                        if (children.Contains(l))
                        {
                            hasChildren.Add(l);
                        }
                    }
                }
            }

            foreach (var l in left)
            {
                if (!hasChildren.Contains(l))
                {
                    Console.WriteLine(l);
                }
            }
        }

        private class Disc
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public Disc Parent { get; set; }
            public List<Disc> Children { get; set; }
            
            public int TotalWeight()
            {
                var w = Weight;
                foreach (var c in Children)
                {
                    w += c.TotalWeight();
                }

                return w;
            }

            public Disc()
            {
                Children = new List<Disc>();
            }
        }

        private static Dictionary<string, List<string>> inputs;
        
        public static void Part2()
        {
            var discLibrary = new Dictionary<string, Disc>();
            inputs = new Dictionary<string, List<string>>();
            string discPattern = @"^([a-z]+) \(([0-9]+)\)";
            Regex rgx = new Regex(discPattern);
            
            var input = File.ReadAllText("Inputs/Day07.txt");
            foreach (var line in input.Split(Environment.NewLine))
            {
                var match = rgx.Match(line);
                if (match.Success)
                {
                    discLibrary.Add(
                        match.Groups[1].Value,
                        new Disc
                        {
                            Name = match.Groups[1].Value,
                            Weight = int.Parse(match.Groups[2].Value)
                        }
                    );
                }
                else
                {
                    throw new Exception(string.Format("{0} did not match {1}", line, discPattern));
                }
            }

            foreach (var line in input.Split(Environment.NewLine))
            {
                var parent = line.Substring(0, line.IndexOf(" "));
                var children = new List<string>();
                if (line.Contains("->"))
                {
                    children = line.Substring(line.IndexOf("->") + 3).Replace(",", "").Split(" ").ToList();
                }

                if (!discLibrary.ContainsKey(parent))
                {
                    throw new Exception(string.Format("parent {0} is not in discLibrary", parent));
                }

                if (children.Count > 0)
                {
                    foreach (var c in children)
                    {
                        if (!discLibrary.ContainsKey(c))
                        {
                            throw new Exception(string.Format("child {0} is not in discLibrary", c));
                        }

                        discLibrary[parent].Children.Add(discLibrary[c]);
                        discLibrary[c].Parent = discLibrary[parent];
                    }
                }
            }

            var root = new Disc();
            foreach (var d in discLibrary)
            {
                if (d.Value.Parent == null)
                {
                    Console.WriteLine("No parent: {0}", d.Key);
                    root = d.Value;
                    break;
                }
            }
            
            Console.WriteLine(root.TotalWeight());
            PrintTreeLevel(root, 1);

        }

        private static void PrintTreeLevel(Disc disc, int level)
        {
            for (var i = 0; i < disc.Children.Count; i++)
            {
                //for (var i = 0; i < level; i++) Console.Write(" ");
                //Console.WriteLine(" {0} ({1})", r.TotalWeight(), r.Children.Count);

                if (i > 0 && disc.Children[i].TotalWeight() != disc.Children[i - 1].TotalWeight())
                {
                    Console.WriteLine("{0} != {1}", disc.Children[i].TotalWeight(), disc.Children[i-1].TotalWeight());
                    if (level == 3)
                    {
                        Console.WriteLine(" {0}", disc.Children[i-2].TotalWeight());
                        Console.WriteLine(" {0}", disc.Children[i-2].Weight);
                        Console.WriteLine(" {0}", disc.Children[i].Weight);
                        foreach (var d in disc.Children[i].Children)
                        {
                            Console.WriteLine("  {0}", d.TotalWeight());
                        }
                        Console.WriteLine(" {0}", disc.Children[i-1].Weight);
                        foreach (var d in disc.Children[i-1].Children)
                        {
                            Console.WriteLine("  {0}", d.TotalWeight());
                        }

                        //Console.WriteLine(disc.Children[i]);
                    }
                }
                
                PrintTreeLevel(disc.Children[i], level + 1);
            }
            /*
            foreach (var r in disc.Children)
            {
                PrintTreeLevel(r, level + 1);
            }
            */
        }

    }
}