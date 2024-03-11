using AdventOfCode.Lib;

namespace AdventOfCode._2015._03;

[Solution("Perfectly Spherical Houses in a Vacuum", 2015, 03)]
public class Solution(string inputPath) : SolutionBase
{
    private string InputPath { get; } = inputPath;
    private string? Input { get; set; }

    public override void PrintSolutions(string ascii1, string ascii2)
    {
        Input = File.ReadAllText(InputPath);

        var houses1 = SolvePartOne();
        Console.WriteLine($"{ascii1} Part 1 => {houses1}");

        var houses2 = SolvePartTwo();
        Console.WriteLine($"{ascii2} Part 2 => {houses2}");
    }

    private int SolvePartOne()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        var houses = new List<(int, int)> { (0, 0) };

        foreach (var direction in input)
        {
            var (x, y) = houses.LastOrDefault();

            switch (direction)
            {
                case '^':
                    houses.Add((x, y + 1));
                    break;
                case '>':
                    houses.Add((x + 1, y));
                    break;
                case 'v':
                    houses.Add((x, y - 1));
                    break;
                case '<':
                    houses.Add((x - 1, y));
                    break;
            }
        }

        return houses.Distinct().Count();
    }

    private int SolvePartTwo()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        var santa = (0, 0);
        var robotSanta = (0, 0);
        var houses = new List<(int, int)> { (0, 0) };

        for (var i = 0; i < input.Length; i++)
        {
            var (x, y) = i % 2 == 0 ? santa : robotSanta;

            switch (input[i])
            {
                case '^':
                    houses.Add((x, y + 1));
                    break;
                case '>':
                    houses.Add((x + 1, y));
                    break;
                case 'v':
                    houses.Add((x, y - 1));
                    break;
                case '<':
                    houses.Add((x - 1, y));
                    break;
            }

            if (i % 2 == 0)
                santa = houses.LastOrDefault();
            else
                robotSanta = houses.LastOrDefault();
        }

        return houses.Distinct().Count();
    }
}