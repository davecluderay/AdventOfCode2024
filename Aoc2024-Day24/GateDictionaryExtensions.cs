namespace Aoc2024_Day24;

internal static class GateDictionaryExtensions
{
    public static Gate[] FindByInputWires(this Dictionary<string, Gate> gates, string input1, string input2)
    {
        return gates.Values.Where(g => (g.InputWire1 == input1 && g.InputWire2 == input2) ||
                                       (g.InputWire1 == input2 && g.InputWire2 == input1))
                    .ToArray();
    }

    public static Gate[] FindByOneInputWire(this Dictionary<string, Gate> gates, string input)
    {
        return gates.Values.Where(g => g.InputWire1 == input || g.InputWire2 == input)
                    .ToArray();
    }

    public static Gate? FindByOutputWire(this Dictionary<string, Gate> gates, string output)
    {
        return gates.GetValueOrDefault(output);
    }
}
