using System.Text.RegularExpressions;

namespace Aoc2024_Day13;

internal readonly partial record struct ClawMachine(int Ax, int Ay, int Bx, int By, long Px, long Py)
{
    public long? FindMinimumCostToWinFast()
    {
        // This only works when there is a single winning combination of button presses.

        // Based on:
        //   Ax * a + Bx * b = Px
        //   Ay * a + By * b = Py
        if (Ax * By - Ay * Bx == 0) return null; // i.e. there are many solutions.
        var b = (Ax * Py - Ay * Px) / (Ax * By - Ay * Bx);
        var a = (Px - Bx * b) / Ax;

        // Check the solution is exact.
        if (a * Ax + b * Bx != Px) return null;
        if (a * Ay + b * By != Py) return null;

        // Pushing button A costs three tokens, B costs one token.
        return a * 3 + b;
    }

    public static IEnumerable<ClawMachine> ReadAll(bool fixConversionErrors)
    {
        var correction = fixConversionErrors ? 10000000000000L : 0L;

        foreach (var section in InputFile.ReadInSections())
        {
            var match = Pattern.Match(string.Join(Environment.NewLine, section));
            if (!match.Success) continue;
            yield return new ClawMachine(
                int.Parse(match.Groups["AX"].Value),
                int.Parse(match.Groups["AY"].Value),
                int.Parse(match.Groups["BX"].Value),
                int.Parse(match.Groups["BY"].Value),
                long.Parse(match.Groups["PX"].Value) + correction,
                long.Parse(match.Groups["PY"].Value) + correction);
        }
    }

    [GeneratedRegex(@"Button A: X\+(?<AX>\d+), Y\+(?<AY>\d+)\s+" +
                    @"Button B: X\+(?<BX>\d+), Y\+(?<BY>\d+)\s+" +
                    @"Prize: X=(?<PX>\d+), Y=(?<PY>\d+)",
                    RegexOptions.Singleline)]
    private static partial Regex Pattern { get; }
}
