using AdventOfCode.Lib;

namespace AdventOfCode._2015._02;

[Solution("I Was Told There Would Be No Math", 2015, 02)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return AggregatePresents(CalcWrappingPaper);

        int CalcWrappingPaper(int x, int y, int z)
        {
            return 2 * (x * y + y * z + x * z) + x * y;
        }
    }

    protected override int SolvePartTwo()
    {
        return AggregatePresents(CalcRibbon);

        int CalcRibbon(int x, int y, int z)
        {
            return 2 * (x + y) + x * y * z;
        }
    }

    private int AggregatePresents(AggregateFunction f)
    {
        return Input.Select(line => line.Split("x").Select(int.Parse).OrderBy(e => e).ToList()).Select(dimensions =>
        {
            int x = dimensions[0], y = dimensions[1], z = dimensions[2];

            return f(x, y, z);
        }).Sum();
    }

    private delegate int AggregateFunction(int x, int y, int z);
}