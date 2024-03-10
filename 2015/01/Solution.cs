using AdventOfCode.Lib;

namespace AdventOfCode._2015._01;

[Solution("Not Quite Lisp", 2015, 01)]
public class Solution(string inputPath) : SolutionBase
{
    private string InputPath { get; } = inputPath;
    private string? Input { get; set; }

    public override void PrintSolutions()
    {
        Input = File.ReadAllText(InputPath);

        var floor = SolvePartOne();
        Console.WriteLine($"\ufe42 Part 1 => {floor}");

        var pos = SolvePartTwo();
        Console.WriteLine($"\ufe42 Part 2 => {pos}");
    }

    private int SolvePartOne()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        return input.Aggregate(0, (acc, e) => e == '(' ? acc + 1 : acc - 1);
    }

    private int SolvePartTwo()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");
        var floor = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (floor == -1)
                return i;

            if (input[i] == '(')
                floor++;
            else
                floor--;
        }

        return -1;
    }
}