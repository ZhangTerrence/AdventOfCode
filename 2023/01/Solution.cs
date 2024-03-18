using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode._2023._01;

[Solution("Trebuchet?!", 2023, 01)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override object SolvePartOne()
    {
        return AggregateStrings(@"\d");
    }

    protected override object SolvePartTwo()
    {
        return AggregateStrings(@"\d|one|two|three|four|five|six|seven|eight|nine");
    }


    private int AggregateStrings(string pattern)
    {
        return Input
            .Select(s => (Regex.Match(s, pattern), Regex.Match(s, pattern, RegexOptions.RightToLeft)))
            .Aggregate(0, (acc, pair) => acc + MatchString(pair.Item1.Value) * 10 + MatchString(pair.Item2.Value));
    }

    private static int MatchString(string s)
    {
        return s switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => int.Parse(s)
        };
    }
}