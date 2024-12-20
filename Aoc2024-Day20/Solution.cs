namespace Aoc2024_Day20;

internal class Solution
{
    public string Title => "Day 20: Race Condition";

    public object PartOne()
    {
        var route = Racetrack.ReadRoute();
        return CountPossibleCheats(route,
                                   maxCheatDuration: 2,
                                   minTimeSaved: 100);
    }

    public object PartTwo()
    {
        var route = Racetrack.ReadRoute();
        return CountPossibleCheats(route,
                                   maxCheatDuration: 20,
                                   minTimeSaved: 100);
    }

    private static int CountPossibleCheats(Position[] route,
                                           int maxCheatDuration,
                                           int minTimeSaved)
    {
        var routeTimes = route.Select((position, index) => (position, index))
                              .ToDictionary(p => p.position, p => p.index);

        var count = 0;
        for (var fromIndex = 0; fromIndex < route.Length; fromIndex++)
        for (var toIndex = fromIndex + 1; toIndex < route.Length; toIndex++)
        {
            var from = route[fromIndex];
            var to = route[toIndex];

            var cheatDuration = from.ManhattanDistance(to);
            if (cheatDuration < 0 || cheatDuration > maxCheatDuration) continue;

            var timeSaved = routeTimes[to] - routeTimes[from] - cheatDuration;
            if (timeSaved < minTimeSaved) continue;

            count++;
        }

        return count;
    }
}
