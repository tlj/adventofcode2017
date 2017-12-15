using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode2017
{
    public class Day13
    {
        private class Firewall
        {
            public int Range { get; set; }
            public int Position { get; set; }
            public int Vector { get; set; }

            public void Move()
            {
                if (Position == Range - 1)
                {
                    Vector = -1;
                }

                if (Position == 0)
                {
                    Vector = 1;
                }

                Position += Vector;
            }
        }
        
        public static void Part1()
        {
            var input = File.ReadAllText("Inputs/Day13.txt");
            var cost = 0;
            var firewalls = new Dictionary<int, Firewall>();
            
            foreach (var line in input.Split(Environment.NewLine))
            {
                var parts = line.Split(":");
                firewalls.Add(int.Parse(parts[0]), new Firewall
                {
                    Range = int.Parse(parts[1].Trim()),
                    Position = 0,
                    Vector = 1
                });
            }

            var maxLayers = firewalls.Max(p => p.Key);
            for (var depth = 0; depth <= maxLayers; depth++)
            {
                if (firewalls.ContainsKey(depth) && firewalls[depth].Position == 0)
                {
                    cost += depth * firewalls[depth].Range;
                }

                foreach (var f in firewalls)
                {
                    f.Value.Move();
                }
            }


            Console.WriteLine("Day 13 part 1: {0}", cost);
        }

        private class Packet
        {
            public int StartedAt { get; set; }
            public int CurrentDepth { get; set; }
            public bool IsDead { get; set; }
        }

        public static void Part2()
        {
            var input = File.ReadAllText("Inputs/Day13.txt");
            
            var firewalls = new Dictionary<int, Firewall>();
            
            foreach (var line in input.Split(Environment.NewLine))
            {
                var parts = line.Split(":");
                firewalls.Add(int.Parse(parts[0]), new Firewall
                {
                    Range = int.Parse(parts[1].Trim()),
                    Position = -1,
                    Vector = 1
                });
            }

            var picosecondDelay = -1;
            var maxLayers = firewalls.Max(p => p.Key);
            var success = false;
            Packet successPacket = new Packet();
            
            var packets = new List<Packet>();

            while (!success)
            {
                picosecondDelay++;

                foreach (var f in firewalls)
                {
                    f.Value.Move();
                }

                packets.Add(new Packet { StartedAt = picosecondDelay, CurrentDepth = -1, IsDead = false });

                var deadPackets = new List<Packet>();
                foreach (var p in packets)
                {
                    p.CurrentDepth++;
                    if (firewalls.ContainsKey(p.CurrentDepth) && firewalls[p.CurrentDepth].Position == 0)
                    {
                        p.IsDead = true;
                        deadPackets.Add(p);
                        continue;
                    }

                    if (p.CurrentDepth == maxLayers)
                    {
                        success = true;
                        successPacket = p;
                        break;
                    }
                }

                foreach (var dp in deadPackets)
                {
                    packets.Remove(dp);
                }
            }


            Console.WriteLine("Day 13 part 2: {0}", successPacket.StartedAt);
        }
    }
}