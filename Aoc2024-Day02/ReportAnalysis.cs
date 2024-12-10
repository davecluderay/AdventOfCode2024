namespace Aoc2024_Day02;

internal class ReportAnalysis
{
    private readonly bool _withDampening;

    public ReportAnalysis(bool withDampening)
    {
        _withDampening = withDampening;
    }

    public bool IsSafe(int[] report)
    {
        if (IsSafe(report.AsEnumerable()))
        {
            return true;
        }

        if (_withDampening)
        {
            return Enumerable.Range(0, report.Length)
                             .Any(skip => IsSafe(report.Where((_, i) => i != skip)));
        }

        return false;
    }

    private static bool IsSafe(IEnumerable<int> report)
    {
        (int? v2, int? v1) = (null, null);

        foreach (var v in report)
        {
            var (d1, d) = (v1 - v2, v - v1);
            (v2, v1) = (v1, v);

            if (!d.HasValue)
            {
                continue;
            }

            if (d == 0 || Math.Abs(d.Value) > 3)
            {
                return false;
            }

            if (d1.HasValue && Math.Sign(d1.Value) != Math.Sign(d.Value))
            {
                return false;
            }
        }

        return true;
    }
}
