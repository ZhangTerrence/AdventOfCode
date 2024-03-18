using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode._2023._02;

[Solution("Cube Conundrum", 2023, 02)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override object SolvePartOne()
    {
        return Input.Select(ParseGames).Sum(pair =>
            pair.Item2[Colors.Red].Max() <= 12 &&
            pair.Item2[Colors.Green].Max() <= 13 &&
            pair.Item2[Colors.Blue].Max() <= 14
                ? pair.Item1
                : 0);
    }

    protected override object SolvePartTwo()
    {
        return Input.Select(ParseGames).Sum(pair =>
            pair.Item2[Colors.Red].Max() *
            pair.Item2[Colors.Green].Max() *
            pair.Item2[Colors.Blue].Max());
    }

    private static (int, Dictionary<Colors, IEnumerable<int>>) ParseGames(string game)
    {
        var match = Regex.Match(game, @"Game (\d+): (.*)");
        if (!match.Success) throw new Exception(game);

        var groups = match.Groups.Cast<Group>().ToList();
        var id = int.Parse(groups[1].Value);
        var subsets = groups[2].Value;

        var cubes = new Dictionary<Colors, IEnumerable<int>>
        {
            { Colors.Red, Regex.Matches(subsets, @"(\d+) red").Select(s => int.Parse(s.Groups[1].Value)) },
            { Colors.Green, Regex.Matches(subsets, @"(\d+) green").Select(s => int.Parse(s.Groups[1].Value)) },
            { Colors.Blue, Regex.Matches(subsets, @"(\d+) blue").Select(s => int.Parse(s.Groups[1].Value)) }
        };

        return (id, cubes);
    }

    private enum Colors
    {
        Red,
        Green,
        Blue
    }
}