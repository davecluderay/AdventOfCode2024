namespace Aoc2024_Day21;

internal class Solution
{
    public string Title => "Day 21: Keypad Conundrum";

    public object PartOne()
    {
        var codes = InputFile.ReadAllLines();
        var keyPads = CreateKeyPads(numberOfDirectionalKeyPads: 2);
        return CalculateComplexityScore(codes, keyPads);
    }

    public object PartTwo()
    {
        var codes = InputFile.ReadAllLines();
        var keyPads = CreateKeyPads(numberOfDirectionalKeyPads: 25);
        return CalculateComplexityScore(codes, keyPads);
    }
    
    private static long CalculateComplexityScore(string[] codes, KeyPad[] keyPads)
    {
        var complexityScore = 0L;
        foreach (var code in codes)
        {
            var codeValue = int.Parse(code.TrimEnd('A'));
            var sequenceLength = KeyPadSolver.CalculateMinimumKeyPresses(code, keyPads);
            complexityScore += codeValue * sequenceLength;
        }
        return complexityScore;
    }

    private static KeyPad[] CreateKeyPads(int numberOfDirectionalKeyPads)
    {
        var keyPads = new KeyPad[numberOfDirectionalKeyPads + 1];
        keyPads[0] = KeyPad.Numeric;
        for (var i = 1; i <= numberOfDirectionalKeyPads; i++)
        {
            keyPads[i] = KeyPad.Directional;
        }
        return keyPads;
    }
}