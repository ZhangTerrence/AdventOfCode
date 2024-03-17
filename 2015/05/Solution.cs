using AdventOfCode.Lib;

namespace AdventOfCode._2015._05;

[Solution("Doesn't He Have Intern-Elves For This?", 2015, 05)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return FilterStrings(line =>
        {
            var threeVowels = line.Count(c => "aeoiu".Contains(c)) >= 3;
            var duplicate = Enumerable.Range(0, line.Length - 1).Any(i => line[i] == line[i + 1]);
            var reserved = "ab.cd.pq.xy".Split(".").Any(line.Contains);

            return threeVowels && duplicate && !reserved;
        });
    }

    protected override int SolvePartTwo()
    {
        return FilterStrings(line =>
        {
            var appearsTwice = Enumerable.Range(0, line.Length - 1)
                .Any(i => line.IndexOf(line.Substring(i, 2), i + 2, StringComparison.Ordinal) >= 0);
            var betweenRepeat = Enumerable.Range(0, line.Length - 2).Any(i => line[i] == line[i + 2]);

            return appearsTwice && betweenRepeat;
        });
    }

    private int FilterStrings(Func<string, bool> f)
    {
        return Input.Count(f);
    }
}