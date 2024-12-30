using System.Text.RegularExpressions;

namespace Aoc2024_Day24;

internal partial record Gate(string InputWire1, string InputWire2, string Op, string OutputWire)
{
    [GeneratedRegex(@"^(?<InputWire1>\w+) (?<Op>XOR|OR|AND) (?<InputWire2>\w+) -> (?<OutputWire>\w+)$")]
    private static partial Regex Pattern { get; }

    public static Gate Parse(string text)
    {
        var match = Pattern.Match(text);
        if (!match.Success) throw new FormatException($"Invalid gate: {text}");
        return new Gate(match.Groups["InputWire1"].Value,
                        match.Groups["InputWire2"].Value,
                        match.Groups["Op"].Value,
                        match.Groups["OutputWire"].Value);
    }
}
