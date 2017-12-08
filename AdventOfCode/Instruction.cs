namespace AdventOfCode
{
    public class Instruction : IInstruction
    {
        public string Register { get; set; }
        public string Operation { get; set; }
        public int Value { get; set; }

        public string Command { get; set; }
        public string ConditionalRegister { get; set; }
        public string Condition { get; set; }
        public int ConditionalValue { get; set; }

        public void Parse(string instruction)
        {
            var parts = instruction.Split(" ");
            Register = parts[0];
            Operation = parts[1];
            Value = int.Parse(parts[2]);
            Command = parts[3];
            ConditionalRegister = parts[4];
            Condition = parts[5];
            ConditionalValue = int.Parse(parts[6]);            
        }
    }
}