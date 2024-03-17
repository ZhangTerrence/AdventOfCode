using AdventOfCode.Lib;

namespace AdventOfCode._2015._02;

[Solution("I Was Told There Would Be No Math", 2015, 02)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return AggregatePresents((x, y, z) => 2 * (x * y + y * z + x * z) + x * y);
    }

    protected override int SolvePartTwo()
    {
        return AggregatePresents((x, y, z) => 2 * (x + y) + x * y * z);
    }

    private int AggregatePresents(Func<int, int, int, int> f)
    {
        return Input
            .Select(line => line.Split("x").Select(int.Parse).OrderBy(e => e).ToList())
            .Aggregate(0, (acc, dimensions) =>
            {
                int x = dimensions[0], y = dimensions[1], z = dimensions[2];
                return acc + f(x, y, z);
            });
    }
}