using AdventOfCode.Lib;

namespace AdventOfCode._2015._01;

[Solution("Not Quite Lisp", 2015, 01)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return TraverseFloors().Last().level;
    }

    protected override int SolvePartTwo()
    {
        return TraverseFloors().First(pair => pair.level == -1).i;
    }

    private IEnumerable<(int i, int level)> TraverseFloors()
    {
        var level = 0;
        for (var j = 0; j < Input[0].Length; j++)
        {
            level += Input[0][j] == '(' ? 1 : -1;
            yield return (j + 1, level);
        }
    }
}