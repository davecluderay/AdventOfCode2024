using System.Text.RegularExpressions;

namespace Aoc2024_Day14;

internal class Solution
{
    public string Title => "Day 14: Restroom Redoubt";

    public object PartOne()
    {
        var space = RobotSpace.Read();
        for (var i = 0; i < 100; i++)
        {
            space.AdvanceOneSecond();
        }
        return space.CalculateSafetyFactor();
    }

    public object PartTwo()
    {
        var space = RobotSpace.Read();
        var elapsed = 0;
        while (!space.HaveRobotsAligned())
        {
            elapsed++;
            space.AdvanceOneSecond();
        }
        return elapsed;
    }
}

internal readonly partial record struct Robot(Vector Position, Vector Velocity)
{
    public static Robot Parse(string text)
    {
        var match = Pattern.Match(text);
        if (!match.Success) throw new FormatException($"Unexpected value: {text}");

        Vector position = new(int.Parse(match.Groups["PX"].Value),
                              int.Parse(match.Groups["PY"].Value));
        Vector velocity = new(int.Parse(match.Groups["VX"].Value),
                              int.Parse(match.Groups["VY"].Value));
        return new Robot(position, velocity);
    }

    [GeneratedRegex(@"^p=(?<PX>-?\d+),(?<PY>-?\d+)\s+v=(?<VX>-?\d+),(?<VY>-?\d+)$")]
    private static partial Regex Pattern { get; }
}

internal readonly record struct Vector(int X, int Y)
{
    public static implicit operator Vector((int X, int Y) tuple) => new(tuple.X, tuple.Y);
}

internal readonly record struct Bounds(int Width, int Height);
