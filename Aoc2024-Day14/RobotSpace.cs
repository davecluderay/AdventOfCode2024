namespace Aoc2024_Day14;

internal class RobotSpace
{
    private readonly Bounds _bounds;
    private readonly List<Robot> _robots;
    private readonly HashSet<Vector> _positions = new();

    private RobotSpace(Bounds bounds, List<Robot> robots)
    {
        _bounds = bounds;
        _robots = robots;

        foreach (var robot in _robots)
        {
            _positions.Add(robot.Position);
        }
    }

    public void AdvanceOneSecond()
    {
        _positions.Clear();
        for (var i = 0; i < _robots.Count; i++)
        {
            var robot = _robots[i];

            var (w, h) = _bounds;
            var (px, py) = robot.Position;
            var (vx, vy) = robot.Velocity;

            var x = (w + px + vx) % w;
            var y = (h + py + vy) % h;
            _robots[i] = robot = robot with { Position = (x, y) };

            _positions.Add(robot.Position);
        }
    }

    public int CalculateSafetyFactor()
    {
        var counts = new Dictionary<Quadrant, int>
                     {
                         [Quadrant.None] = 0,
                         [Quadrant.TopLeft] = 0,
                         [Quadrant.TopRight] = 0,
                         [Quadrant.BottomLeft] = 0,
                         [Quadrant.BottomRight] = 0
                     };

        foreach (var robot in _robots)
        {
            counts[GetQuadrant(robot.Position)]++;
        }

        return counts.Where(c => c.Key != Quadrant.None)
                     .Aggregate(1, (a, v) => a * v.Value);
    }

    public bool HaveRobotsAligned()
    {
        return EasterEggFinder.IsEasterEgg(_positions);
    }

    private Quadrant GetQuadrant(Vector position)
    {
        var (x, y) = position;
        var (midX, midY) = (_bounds.Width / 2, _bounds.Height / 2);
        if (x < midX && y < midY) return Quadrant.TopLeft;
        if (x < midX && y > midY) return Quadrant.BottomLeft;
        if (x > midX && y < midY) return Quadrant.TopRight;
        if (x > midX && y > midY) return Quadrant.BottomRight;
        return Quadrant.None;
    }

    public static RobotSpace Read()
    {
        var robots = InputFile.ReadAllLines()
                              .Select(Robot.Parse)
                              .ToList();

        var maxX = robots.Max(r => r.Position.X);
        var maxY = robots.Max(r => r.Position.Y);
        Bounds bounds = maxX >= 11 || maxY >= 7 ? new(101, 103) : new(11, 7);

        return new RobotSpace(bounds, robots);
    }

    private enum Quadrant { None, TopLeft, TopRight, BottomLeft, BottomRight }
}
