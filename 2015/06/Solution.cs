using AdventOfCode.Lib;

namespace AdventOfCode._2015._06;

[Solution("Probably a Fire Hazard", 2015, 06)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return FollowInstructions(1, Light);

        int Light(int light)
        {
            return light;
        }
    }

    protected override int SolvePartTwo()
    {
        return FollowInstructions(2, Brightness);

        int Brightness(int light)
        {
            return light;
        }
    }

    private int FollowInstructions(int part, AggregateFunction f)
    {
        var grid = new int[1000, 1000];

        foreach (var words in Input.Select(instruction => instruction.Split()))
            if (words[0] == "toggle")
            {
                ChangeLights(grid, words[1], words[3], part == 1 ? Actions.ToggleOne : Actions.ToggleTwo);
            }
            else
            {
                var action = string.Join(" ", words[..2]);

                switch (action)
                {
                    case "turn on":
                        ChangeLights(grid, words[2], words[4], part == 1 ? Actions.OnOne : Actions.OnTwo);
                        break;
                    case "turn off":
                        ChangeLights(grid, words[2], words[4], part == 1 ? Actions.OffOne : Actions.OffTwo);
                        break;
                }
            }

        return Enumerable.Range(0, 1000).Aggregate(0,
            (accI, i) => accI + Enumerable.Range(0, 1000).Aggregate(0, (accJ, j) => accJ + f(grid[i, j])));
    }

    private static void ChangeLights(int[,] grid, string start, string end, Actions action)
    {
        var startPos = start.Split(",").Select(int.Parse).ToList();
        var endPos = end.Split(",").Select(int.Parse).ToList();

        for (var startY = startPos[1]; startY <= endPos[1]; startY++)
        for (var startX = startPos[0]; startX <= endPos[0]; startX++)
            grid[startX, startY] = action switch
            {
                Actions.OnOne => 1,
                Actions.OffOne => 0,
                Actions.ToggleOne => 1 - grid[startX, startY],
                Actions.OnTwo => grid[startX, startY] += 1,
                Actions.OffTwo => grid[startX, startY] == 0 ? 0 : grid[startX, startY] -= 1,
                Actions.ToggleTwo => grid[startX, startY] += 2,
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };
    }

    private enum Actions
    {
        OnOne,
        OffOne,
        ToggleOne,
        OnTwo,
        OffTwo,
        ToggleTwo
    }

    private delegate int AggregateFunction(int light);
}