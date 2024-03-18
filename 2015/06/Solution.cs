using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._06;

[Solution("Probably a Fire Hazard", 2015, 06)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override object SolvePartOne()
    {
        return FollowInstructions(_ => 1, _ => 0, n => 1 - n);
    }

    protected override object SolvePartTwo()
    {
        return FollowInstructions(n => n + 1, n => n <= 0 ? 0 : n - 1, n => n + 2);
    }

    private int FollowInstructions(Func<int, int> turnOn, Func<int, int> turnOff, Func<int, int> toggle)
    {
        return Input.Aggregate(new int[1000 * 1000], (grid, instruction) =>
            ApplyInstruction(grid, instruction, @"turn on (\d+),(\d+) through (\d+),(\d+)", turnOn) ??
            ApplyInstruction(grid, instruction, @"turn off (\d+),(\d+) through (\d+),(\d+)", turnOff) ??
            ApplyInstruction(grid, instruction, @"toggle (\d+),(\d+) through (\d+),(\d+)", toggle) ??
            throw new Exception(instruction)).Sum();
    }

    private static int[]? ApplyInstruction(int[] grid, string instruction, string pattern, Func<int, int> action)
    {
        var match = Regex.Match(instruction, pattern);
        if (!match.Success) return null;

        var groups = match.Groups.Cast<Group>().Skip(1).Select(e => int.Parse(e.Value)).ToList();
        List<int> start = groups[..2], end = groups[2..4];

        for (var i = start[0]; i <= end[0]; i++)
        for (var j = start[1]; j <= end[1]; j++)
            grid[i * 1000 + j] = action(grid[i * 1000 + j]);

        return grid;
    }
}