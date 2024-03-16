using System.Text;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._10;

[Solution("Elves Look, Elves Say", 2015, 10)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return LookAndSay(Input[0]).Skip(39).First().Length;
    }

    protected override int SolvePartTwo()
    {
        return LookAndSay(Input[0]).Skip(49).First().Length;
    }

    private static IEnumerable<string> LookAndSay(string input)
    {
        while (true)
        {
            var newString = new StringBuilder();

            for (var i = 0; i < input.Length;)
            {
                if (i == input.Length - 1)
                {
                    newString.Append($"1{input[i]}");
                    break;
                }

                int j = 0, count = 0;
                while (i + j < input.Length && input[i] == input[i + j])
                {
                    count++;
                    j++;
                }

                newString.Append($"{count}{input[i]}");
                i += j;
            }

            input = newString.ToString();
            yield return input;
        }
    }
}