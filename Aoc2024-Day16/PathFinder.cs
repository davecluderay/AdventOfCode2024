namespace Aoc2024_Day16;

internal static class PathFinder
{
    public static (int MinimumCost, HashSet<Vector> Positions) CalculateLowestCostPath(
        HashSet<GraphNode> nodes,
        ILookup<GraphNode, GraphEdge> edges,
        GraphNode startGraphNode,
        Vector endPosition)
    {
        var costs = nodes.ToDictionary(x => x, _ => (Via: new HashSet<GraphNode>(), Cost: int.MaxValue));
        costs[startGraphNode] = ([], 0);

        var queue = new PriorityQueue<GraphNode, int>([(startGraphNode, 0)]);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            var currentNodeCost = costs[current].Cost;
            foreach (var edge in edges[current])
            {
                var oldLowestCost = costs[edge.To].Cost;
                int newLowestCost = currentNodeCost + edge.Cost;
                if (newLowestCost == oldLowestCost)
                {
                    costs[edge.To].Via.Add(current);
                }
                else if (newLowestCost < oldLowestCost)
                {
                    costs[edge.To].Via.Clear();
                    costs[edge.To].Via.Add(current);
                    costs[edge.To] = costs[edge.To] with { Cost = newLowestCost };
                    queue.Remove(edge.To, out _, out _);
                    queue.Enqueue(edge.To, newLowestCost);
                }
            }
        }
        
        var minimumCost = costs.Where(c => c.Key.Position == endPosition)
                               .Min(c => c.Value.Cost);
        var positions = FindPositionsAlongLowestCostPaths(costs.Where(c => c.Key.Position == endPosition &&
                                                                     c.Value.Cost == minimumCost)
                                                         .Select(c => c.Key),
                                                    costs);
        return (minimumCost, positions);
    }

    private static HashSet<Vector> FindPositionsAlongLowestCostPaths(IEnumerable<GraphNode> endNodes, Dictionary<GraphNode, (HashSet<GraphNode> Via, int Cost)> costs)
    {
        HashSet<Vector> positions = new();

        HashSet<GraphNode> visited = new();
        var queue = new Queue<GraphNode>(endNodes);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            if (!visited.Add(node)) continue;

            foreach (var via in costs[node].Via)
            {
                // Add positions between node and via.
                Vector moveVector = (Math.Sign(via.Position.X - node.Position.X), Math.Sign(via.Position.Y - node.Position.Y));
                Vector current = node.Position;
                while (current != via.Position)
                {
                    positions.Add(current);
                    current += moveVector;
                }
                positions.Add(via.Position);
                queue.Enqueue(via);
            }
        }

        return positions;
    }
}

internal record GraphNode(Vector Position, Direction Direction)
{
    public static implicit operator GraphNode((Vector Position, Direction Direction) tuple)
        => new(tuple.Position, tuple.Direction);
}

internal record GraphEdge(GraphNode From, GraphNode To, int Cost)
{
    public static implicit operator GraphEdge((GraphNode From, GraphNode To, int Cost) tuple)
        => new(tuple.From, tuple.To, tuple.Cost);
}
