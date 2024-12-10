namespace Aoc2024_Day05;

internal sealed class RulesBasedComparer : IComparer<int>
{
    private readonly HashSet<Rule> _rules;

    public RulesBasedComparer(IEnumerable<Rule> rules)
    {
        _rules = rules.ToHashSet();
    }

    public int Compare(int x, int y)
    {
        return _rules.Contains(new(x, y))
                   ? -1
                   : _rules.Contains(new(y, x))
                       ? 1
                       : 0;
    }
}
