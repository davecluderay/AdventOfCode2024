namespace Aoc2024_Day16;

internal class ReindeerMaze
{
    private readonly Dictionary<Vector, char> _data;
    private readonly Vector _start;
    private readonly Vector _end;

    private ReindeerMaze(Dictionary<Vector, char> data, Vector start, Vector end)
        => (_data, _start, _end) = (data, start, end);

    public int FindBestRouteScore()
    {
        var (nodes, edges) = BuildGraph();
        var startNode = new GraphNode(_start, Direction.East);
        var endPosition = _end;
        return PathFinder.CalculateLowestCostPath(nodes, edges, startNode, endPosition)
                         .MinimumCost;
    }

    public int CountPositionsAlongBestPaths()
    {
        var (nodes, edges) = BuildGraph();
        var startNode = new GraphNode(_start, Direction.East);
        var endPosition = _end;
        return PathFinder.CalculateLowestCostPath(nodes, edges, startNode, endPosition)
                         .Positions.Count;
    }

    private (HashSet<GraphNode> Nodes, ILookup<GraphNode, GraphEdge> Edges) BuildGraph()
    {
        // Build a graph of the maze, where nodes consist of a position and facing direction.
        // Each turn is considered a node, and each straight path is considered a node.
        // The cost of a turn is 1000 points, and the cost of a straight path is the distance.
        GraphNode startNode = (_start, Direction.East);
        HashSet<GraphNode> nodes = [];
        HashSet<GraphEdge> edges = [];
        var queue = new Queue<GraphNode>([startNode]);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            if (!nodes.Add(node)) continue;

            foreach (var dc in node.Direction.GetRelativeDirectionCosts())
            {
                // Only consider direction changes, and those that are not immediately obstructed.
                if (dc.Direction == node.Direction) continue;
                if (!IsAccessible(node.Position + dc.Direction.AsVector())) continue;

                var nextNode = node with { Direction = dc.Direction };
                edges.Add((From: node, To: nextNode, dc.Cost));
                queue.Enqueue(nextNode);
            }

            var distance = 0;
            var vector = node.Direction.AsVector();
            var nextPosition = node.Position;
            while (IsAccessible(nextPosition + vector))
            {
                nextPosition += vector;
                distance++;
                if (IsJunction(nextPosition) || IsEnd(nextPosition))
                {
                    break;
                }
            }

            if (distance > 0)
            {
                GraphNode nextNode = (nextPosition, node.Direction);
                edges.Add((From: node, To: nextNode, Cost: distance));
                queue.Enqueue(nextNode);
            }
        }

        return (nodes, edges.ToLookup(e => e.From));
    }

    private bool IsAccessible(Vector at)
        => !_data.TryGetValue(at, out var c) || c != '#';

    private bool IsEnd(Vector at)
        => at == _end;

    private bool IsJunction(Vector at)
    {
        var count = 0;
        if (IsAccessible(at + Direction.North.AsVector())) count++;
        if (IsAccessible(at + Direction.East.AsVector())) count++;
        if (IsAccessible(at + Direction.South.AsVector())) count++;
        if (IsAccessible(at + Direction.West.AsVector())) count++;
        return count > 2;
    }

    public static ReindeerMaze Read()
    {
        Dictionary<Vector, char> data = new();
        Vector start = default;
        Vector end = default;

        var lines = InputFile.ReadAllLines();
        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
        {
            switch (lines[y][x])
            {
                case '#':
                    data[(x, y)] = '#';
                    break;
                case 'S':
                    start = (x, y);
                    break;
                case 'E':
                    end = (x, y);
                    break;
            }
        }

        return new ReindeerMaze(data, start, end);
    }
}
