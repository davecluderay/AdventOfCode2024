using System.Collections.Immutable;

namespace Aoc2024_Day23;

internal class Solution
{
    public string Title => "Day 23: LAN Party";

    public object PartOne()
    {
        var connections = ReadConnections();

        var results = new HashSet<(string A, string B, string C)>();
        foreach (var a in connections.Keys)
        foreach (var b in connections[a])
        foreach (var c in connections[a])
        {
            if (c == b) continue;
            if (!connections[c].Contains(b)) continue;
            if (!a.StartsWith('t') && !b.StartsWith('t') && !c.StartsWith('t')) continue;
        
            string[] ordered = [a, b, c];
            Array.Sort(ordered);
            results.Add((ordered[0], ordered[1], ordered[2]));
        }

        return results.Count;
    }

    public object PartTwo()
    {
        var connections = ReadConnections();
        var cliques = FindMaximalCliques(connections);
        var maximumClique = cliques.MaxBy(c => c.Count)!;
        return string.Join(',', maximumClique.Order());
    }

    private static IEnumerable<ISet<string>> FindMaximalCliques(Dictionary<string, HashSet<string>> connections)
    {
        return BronKerboschWithPivoting(ImmutableHashSet<string>.Empty,
                           connections.Keys.ToImmutableHashSet(),
                           ImmutableHashSet<string>.Empty);

        IEnumerable<ISet<string>> BronKerboschWithPivoting(ImmutableHashSet<string> r, ImmutableHashSet<string> p, ImmutableHashSet<string> x)
        {
            if (p.IsEmpty && x.IsEmpty)
            {
                yield return r;
                yield break;
            }

            var u = p.Union(x).First();
            foreach (var v in p.Except(connections[u]))
            {
                foreach (var clique in BronKerboschWithPivoting(r.Add(v),
                                                                p.Intersect(connections[v]),
                                                                x.Intersect(connections[v])))
                {
                    yield return clique;
                }

                p = p.Remove(v);
                x = x.Add(v);
            }
        }
    }

    private static Dictionary<string, HashSet<string>> ReadConnections()
    {
        Dictionary<string, HashSet<string>> results = new();

        foreach (var connection in InputFile.ReadAllLines()
                                            .Select(s => s.Split('-')))
        {
            var (c1, c2) = (connection[0], connection[1]);

            results.GetOrAdd(c1, () => new())
                   .Add(c2);
            results.GetOrAdd(c2, () => new())
                   .Add(c1);
        }

        return results;
    }
}
