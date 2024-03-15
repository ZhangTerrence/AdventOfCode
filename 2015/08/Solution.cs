using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._08;

[Solution("Matchsticks", 2015, 08)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return CalculateDifference(
            e => e.Replace(@"\\", ".").Replace(@"\""", "."),
            e => Regex.Replace(e, @"\\x[a-z0-9]{2}", "."),
            e => e.Substring(1, e.Length - 2));
    }

    protected override int SolvePartTwo()
    {
        return CalculateDifference(
            e => e.Replace(@"\", @"\\").Replace(@"""", @"\"""),
            e => Regex.Replace(e, @"(\\x[a-z0-9]{2})$", "\\$1"),
            e => $@"""{e}""");
    }

    private int CalculateDifference(AggregateFunction x, AggregateFunction y, AggregateFunction z)
    {
        var original = Input.Aggregate(0, (acc, e) => acc + e.Length);
        var altered = Input
            .Select(e => x(e)).Select(e => y(e)).Select(e => z(e))
            .Aggregate(0, (acc, e) => acc + e.Length);

        return Math.Abs(original - altered);
    }

    private delegate string AggregateFunction(string s);
}