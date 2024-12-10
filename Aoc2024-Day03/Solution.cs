using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024_Day03;

internal partial class Solution
{
    public string Title => "Day 3: Mull It Over";

    public object PartOne()
    {
        return ProcessInstructions(ignoreConditionals: true);
    }

    public object PartTwo()
    {
        return ProcessInstructions(ignoreConditionals: false);
    }

    private static int ProcessInstructions(bool ignoreConditionals)
    {
        int result = 0;
        var @do = true;
        foreach (var command in ReadInput())
        {
            switch (command.Command)
            {
                case "do":
                    @do = true;
                    break;
                case "don't":
                    if (ignoreConditionals) continue;
                    @do = false;
                    break;
                case "mul":
                    if (!@do) continue;
                    result += command.Left * command.Right;
                    break;
                default:
                    throw new UnreachableException();
            }
        }
        return result;
    }
    private static (string Command, int Left, int Right)[] ReadInput()
    {
        var text = InputFile.ReadAllText();
        var matches = MulPattern.Matches(text);

        return matches.Select(m => (command: m.Groups["Cmd"].Value,
                                    left: int.TryParse(m.Groups["Left"].Value, out var l) ? l : 0,
                                    right: int.TryParse(m.Groups["Right"].Value, out var r) ? r : 0))
                      .ToArray();
    }

    [GeneratedRegex(@"((?<Cmd>mul)\((?<Left>\d{1,3}),(?<Right>\d{1,3})\)|(?<Cmd>do|don't)\(\))", RegexOptions.ExplicitCapture)]
    private static partial Regex MulPattern { get; }
}
