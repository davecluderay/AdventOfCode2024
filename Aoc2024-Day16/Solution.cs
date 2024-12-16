namespace Aoc2024_Day16;

internal class Solution
{
    public string Title => "Day 16: Reindeer Maze";

    public object PartOne()
    {
        var maze = ReindeerMaze.Read();
        var score = maze.FindBestRouteScore();
        return score;
    }

    public object PartTwo()
    {
        var maze = ReindeerMaze.Read();
        var count = maze.CountPositionsAlongBestPaths();
        return count;
    }
}
