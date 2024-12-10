namespace Aoc2024_Day04;

internal class Solution
{
    public string Title => "Day 4: Ceres Search";

    public object PartOne()
    {
        Span<(int dy, int dx)> directions = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];

        var puzzle = InputFile.ReadAllLines();
        var count = 0;
        for (var x = 0; x < puzzle[0].Length; x++)
        for (var y = 0; y < puzzle.Length; y++)
        {
            const string word = "XMAS";
            if (puzzle[y][x] != word[0]) continue;

            foreach (var d in directions)
            {
                for (var n = 1; n < word.Length; n++)
                {
                    var ny = y + d.dy * n;
                    if (ny < 0 || ny >= puzzle.Length) break;

                    var nx = x + d.dx * n;
                    if (nx < 0 || nx >= puzzle[0].Length) break;

                    if (puzzle[ny][nx] != word[n]) break;

                    if (n != word.Length - 1) continue;

                    count++;
                }
            }
        }
        return count;
    }

    public object PartTwo()
    {
        var puzzle = InputFile.ReadAllLines();
        var count = 0;
        for (var x = 1; x < puzzle[0].Length - 1; x++)
        for (var y = 1; y < puzzle.Length - 1; y++)
        {
            if (puzzle[y][x] != 'A') continue;

            var (a, b) = (puzzle[y - 1][x - 1], puzzle[y + 1][x + 1]);
            if (a > b) (a, b) = (b, a);
            if (a != 'M' || b != 'S') continue;

            (a, b) = (puzzle[y - 1][x + 1], puzzle[y + 1][x - 1]);
            if (a > b) (a, b) = (b, a);
            if (a != 'M' || b != 'S') continue;

            count++;
        }
        return count;
    }
}
