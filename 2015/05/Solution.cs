using AdventOfCode.Lib;

namespace AdventOfCode._2015._05;

[Solution("Doesn't He Have Intern-Elves For This?", 2015, 05)]
public class Solution(string inputPath) : SolutionBase
{
    private string InputPath { get; } = inputPath;
    private List<string>? Input { get; set; }

    public override void PrintSolutions(string ascii1, string ascii2)
    {
        Input = File.ReadLines(InputPath).ToList();

        var stringAmount1 = SolvePartOne();
        Console.WriteLine($"{ascii1} Part 1 => {stringAmount1}");

        var stringAmount2 = SolvePartTwo();
        Console.WriteLine($"{ascii2} Part 2 => {stringAmount2}");
    }

    private int SolvePartOne()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        return input.Count(line =>
        {
            var threeVowels = line.Count(c => "aeoiu".Contains(c)) >= 3;
            var duplicate = Enumerable.Range(0, line.Length - 1).Any(i => line[i] == line[i + 1]);
            var reserved = "ab.cd.pq.xy".Split(".").Any(line.Contains);

            return threeVowels && duplicate && !reserved;
        });
    }

    private int SolvePartTwo()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        return input.Count(line =>
        {
            var appearsTwice = Enumerable.Range(0, line.Length - 1)
                .Any(i => line.IndexOf(line.Substring(i, 2), i + 2, StringComparison.Ordinal) >= 0);
            var betweenRepeat = Enumerable.Range(0, line.Length - 2).Any(i => line[i] == line[i + 2]);

            return appearsTwice && betweenRepeat;
        });
    }
}