using AdventOfCode.Lib;

namespace AdventOfCode._2015._02;

[Solution("I Was Told There Would Be No Math", 2015, 02)]
public class Solution(string inputPath) : SolutionBase
{
    private string InputPath { get; } = inputPath;
    private List<string>? Input { get; set; }

    public override void PrintSolutions(string ascii1, string ascii2)
    {
        Input = File.ReadLines(InputPath).ToList();

        var wrappingPaper = SolvePartOne();
        Console.WriteLine($"{ascii1} Part 1 => {wrappingPaper}");

        var ribbon = SolvePartTwo();
        Console.WriteLine($"{ascii2} Part 2 => {ribbon}");
    }

    private int SolvePartOne()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        return input.Select(line =>
                line.Split("x").Select(int.Parse).OrderBy(e => e).ToList())
            .Aggregate(0,
                (acc, dimensions) =>
                {
                    int x = dimensions[0], y = dimensions[1], z = dimensions[2];

                    var wrappingPaper = 2 * x * y +
                                        2 * y * z +
                                        2 * x * z +
                                        x * y;

                    acc += wrappingPaper;

                    return acc;
                });
    }

    private int SolvePartTwo()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        return input.Select(line => line.Split("x").Select(int.Parse).OrderBy(e => e).ToList()).Aggregate(0,
            (acc, dimensions) =>
            {
                int x = dimensions[0], y = dimensions[1], z = dimensions[2];

                var ribbon = 2 * x + 2 * y + x * y * z;

                acc += ribbon;

                return acc;
            });
    }
}