using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Computer
    {
        public Dictionary<string, int> Registers { get; }

        public Computer()
        {
            Registers = new Dictionary<string, int>();
        }

        public void Run(IEnumerable<string> instructions, IInstruction ip)
        {
            foreach (var instruction in instructions)
            {
                ip.Parse(instruction);
                RunInstruction(ip);
            }
        }

        public void RunInstruction(IInstruction ip)
        {
            AddRegister(ip.Register);
            AddRegister(ip.ConditionalRegister);

            switch (ip.Command)
            {
                case "if":
                    if (If(ip.ConditionalRegister, ip.Condition, ip.ConditionalValue)) {
                        DoOperation(ip.Register, ip.Operation, ip.Value);                        
                    }
                    break;
                default:
                    throw new Exception(string.Format("Invalid command {0}", ip.Command));
            }
        }

        private void DoOperation(string register, string op, int value)
        {
            switch (op)
            {
                case "inc":
                    Registers[register] += value;
                    break;
                case "dec":
                    Registers[register] -= value;
                    break;
                default:
                    throw new Exception(string.Format("Invalid operation {0}", op));                        
            }
        }

        private void AddRegister(string register)
        {
            if (!Registers.ContainsKey(register))
            {
                Registers[register] = 0;
            }
        }

        private bool If(string register, string condition, int value)
        {
            switch (condition)
            {
                case "<": return Registers[register] < value;
                case ">": return Registers[register] > value;
                case "==": return Registers[register] == value;
                case "<=": return Registers[register] <= value;
                case ">=": return Registers[register] >= value;
                case "!=": return Registers[register] != value;
                default:
                    throw new Exception(string.Format("Invalid condition {0}", condition));
            }
        }
        
    }
}