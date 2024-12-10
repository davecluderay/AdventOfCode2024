namespace Aoc2024_Day05;

internal readonly record struct Update(int[] Pages)
{
    public int MiddlePageNumber => Pages[Pages.Length / 2];

    public bool SatisfiesAll(Rule[] rules)
    {
        foreach (var rule in rules)
        {
            var firstIndex = Array.IndexOf(Pages, rule.First);
            if (firstIndex < 0) continue;

            var secondIndex = Array.IndexOf(Pages, rule.Second);
            if (secondIndex < 0) continue;

            if (firstIndex > secondIndex)
            {
                return false;
            }
        }
        return true;
    }

    public static Update Parse(string line)
    {
        var pages = line.Split(',');
        return new Update(pages.Select(int.Parse).ToArray());
    }

    public static Update Correct(Update incorrect, Rule[] rules)
    {
        rules = rules.Where(r => incorrect.Pages.Contains(r.First) && incorrect.Pages.Contains(r.Second)).ToArray();
        var pages = incorrect.Pages.Order(new RulesBasedComparer(rules)).ToArray();
        return new Update(pages);
    }
}
