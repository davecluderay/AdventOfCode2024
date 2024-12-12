using System.Text.RegularExpressions;

namespace Aoc2024_Day07;

internal readonly partial record struct Calibration(long TestValue, long[] Numbers)
{
    public bool CanBeCorrect(bool includeCat)
    {
        ReadOnlySpan<Func<long, long, long>> operators = includeCat
            ? [Operator.Add, Operator.Mul, Operator.Cat]
            : [Operator.Add, Operator.Mul];

        var result = Try(Numbers[0], Numbers[1..], operators, TestValue);
        return result.HasValue;

        static long? Try(long current, ReadOnlySpan<long> remaining, ReadOnlySpan<Func<long, long, long>> operators, long target)
        {
            if (current == target && remaining.IsEmpty) return current;
            if (remaining.IsEmpty) return null;
            foreach (var op in operators)
            {
                var result = op(current, remaining[0]);
                if (result > target) continue;
                var next = Try(result, remaining[1..], operators, target);
                if (next.HasValue) return next;
            }
            return null;
        }
    }
    public static Calibration Parse(string text)
    {
        var match = Pattern.Match(text);
        if (!match.Success) throw new ArgumentException("Unrecognized format", nameof(text));
        var testValue = Convert.ToInt64(match.Groups["TestValue"].Value);
        var numbers = match.Groups["Numbers"].Captures.Select(c => Convert.ToInt64(c.Value)).ToArray();
        return new Calibration(testValue, numbers);
    }

    [GeneratedRegex(@"^(?<TestValue>\d+):(\s+(?<Numbers>\d+))+$", RegexOptions.ExplicitCapture)]
    private static partial Regex Pattern { get; }
}
