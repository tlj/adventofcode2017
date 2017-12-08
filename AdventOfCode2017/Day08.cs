using System;
using System.IO;
using System.Linq;
using AdventOfCode;

namespace AdventOfCode2017
{
    public static class Day08
    {
       
        public static void Part1()
        {
            var computer = new Computer();
            var ip = new Instruction();
            
            var high2 = 0;
            var input = File.ReadAllText("Inputs/Day08.txt");
            
            foreach (var instruction in input.Split(Environment.NewLine))
            {
                ip.Parse(instruction);
                computer.RunInstruction(ip);
                var highest = computer.Registers.Max(p => p.Value);
                high2 = highest > high2 ? highest : high2;
            }

            Console.WriteLine(computer.Registers.Max(r => r.Value));
            Console.WriteLine(high2);
        }
        
    }
}