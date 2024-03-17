using AdventOfCode.Lib;

namespace AdventOfCode._2015._09;

[Solution("All in a Single Night", 2015, 09)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return FindDistances(Action.Min).MinBy(pair => pair.Value).Value;
    }

    protected override int SolvePartTwo()
    {
        return FindDistances(Action.Max).MaxBy(pair => pair.Value).Value;
    }

    private Dictionary<string, int> FindDistances(Action action)
    {
        var edges = Input.Select(edge => edge.Split().Where((_, i) => i % 2 == 0).ToList()).ToList();
        var vertices = edges.SelectMany(edge => edge[..^1]).Distinct().ToList();
        var distances = new Dictionary<string, int>();

        foreach (var vertex in vertices) distances[vertex] = FindDistance(vertex, action, edges, vertices);

        return distances;
    }

    private static int FindDistance(string source, Action action, List<List<string>> edges, List<string> vertices)
    {
        var seen = new HashSet<string> { source };
        var current = source;

        var distance = 0;
        while (seen.Count != vertices.Count)
        {
            var currentCopy = current;
            var matchedEdges = edges.Where(e =>
                !(seen.Contains(e[0]) && seen.Contains(e[1])) && (e[0] == currentCopy || e[1] == currentCopy));
            var edge = action == Action.Min
                ? matchedEdges.MinBy(e => int.Parse(e[2]))
                : matchedEdges.MaxBy(e => int.Parse(e[2]));
            if (edge is null) break;

            seen.Add(edge[0] == current ? edge[1] : edge[0]);
            current = edge[0] == current ? edge[1] : edge[0];
            distance += int.Parse(edge[2]);
        }

        return distance;
    }

    private enum Action
    {
        Min,
        Max
    }
}