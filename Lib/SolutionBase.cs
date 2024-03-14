namespace AdventOfCode.Lib;

public abstract class SolutionBase(string inputPath)
{
    private string InputPath { get; } = inputPath;
    protected List<string> Input { get; private set; } = null!;

    public void PrintSolutions(string x, string y)
    {
        Input = File.ReadLines(InputPath).ToList();

        var solutionOne = SolvePartOne();
        Console.WriteLine($"{x} Part 1 => {solutionOne}");

        var solutionTwo = SolvePartTwo();
        Console.WriteLine($"{y} Part 2 => {solutionTwo}");
    }

    protected abstract int SolvePartOne();

    protected abstract int SolvePartTwo();
}