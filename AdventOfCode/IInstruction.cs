namespace AdventOfCode
{
    public interface IInstruction
    {
        string Register { get; set; }
        string Operation { get; set; }
        int Value { get; set; }

        string Command { get; }
        string ConditionalRegister { get; set; }
        string Condition { get; set; }
        int ConditionalValue { get; set; }

        void Parse(string instruction);
    }
}