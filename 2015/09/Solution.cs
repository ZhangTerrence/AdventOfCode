using AdventOfCode.Lib;

namespace AdventOfCode._2015._09;

[Solution("All in a Single Night", 2015, 09)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return FindDistance("Faerun");
    }

    protected override int SolvePartTwo()
    {
        return FindDistance("Arbre");
    }

    private List<List<string>> ParseEdges()
    {
        return Input.Select(edge => edge.Split().Where((_, i) => i % 2 == 0).ToList()).ToList();
    }

    private int FindDistance(string source)
    {
        var edges = ParseEdges();
        var vertices = edges.SelectMany(e => e[..^1]).Distinct().ToList();
        var seen = new HashSet<string> { source };
        var current = source;
        
        var distance = 0;
        while (seen.Count != vertices.Count)
        {
            var conditions = new Func<List<string>, string, bool>((e, c) =>
            {
                var seenAlready = seen.Contains(e[0]) && seen.Contains(e[1]);
                var reachable = e[0] == c || e[1] == c;

                return !seenAlready && reachable;
            });
            
            var edge = edges.Where(e => conditions(e, current)).MaxBy(e => int.Parse(e[2]));
            if (edge == null) break;

            if (edge[0] == current)
            {
                seen.Add(edge[1]);
                current = edge[1];
            }
            else
            {
                seen.Add(edge[0]);
                current = edge[0];
            }

            distance += int.Parse(edge[2]);
        }

        return distance;
    }
}