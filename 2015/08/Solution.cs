using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._08;

[Solution("Matchsticks", 2015, 08)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return CalculateDifference(
            s => s.Replace(@"\\", ".").Replace(@"\""", "."),
            s => Regex.Replace(s, @"\\x[a-z0-9]{2}", "."),
            s => s.Substring(1, s.Length - 2));
    }

    protected override int SolvePartTwo()
    {
        return CalculateDifference(
            s => s.Replace(@"\", @"\\").Replace(@"""", @"\"""),
            s => Regex.Replace(s, @"(\\x[a-z0-9]{2})$", "\\$1"),
            s => $@"""{s}""");
    }

    private int CalculateDifference(AggregateFunction x, AggregateFunction y, AggregateFunction z)
    {
        var original = Input.Aggregate(0, (acc, s) => acc + s.Length);
        var altered = Input
            .Select(s => x(s)).Select(s => y(s)).Select(s => z(s))
            .Aggregate(0, (acc, s) => acc + s.Length);
        return Math.Abs(original - altered);
    }

    private delegate string AggregateFunction(string s);
}