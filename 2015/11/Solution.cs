using System.Text;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._11;

[Solution("Corporate Policy", 2015, 11)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override object SolvePartOne()
    {
        return Passwords(Input[0]).Where(FilterPassword).First();
    }

    protected override object SolvePartTwo()
    {
        return Passwords(Input[0]).Where(FilterPassword).Skip(1).First();
    }

    private static bool FilterPassword(string password)
    {
        var straight = Enumerable.Range(0, password.Length - 2).Any(i =>
            password[i] + 1 == password[i + 1] && password[i] + 2 == password[i + 2]);
        var restricted = "iol".Any(password.Contains);
        var pairs = Enumerable.Range(0, password.Length - 1).Select(i => password.Substring(i, 2))
            .Where(pair => pair[0] == pair[1]).Distinct().Count() >= 2;

        return straight && !restricted && pairs;
    }

    private static IEnumerable<string> Passwords(string input)
    {
        while (true)
        {
            var password = new StringBuilder();

            for (var i = input.Length - 1; i >= 0; i--)
            {
                var nextLetter = input[i] + 1;

                if (nextLetter > 'z')
                {
                    nextLetter = 'a';
                    password.Insert(0, (char)nextLetter);
                }
                else
                {
                    password.Insert(0, (char)nextLetter);
                    password.Insert(0, input.AsSpan(0, i));
                    break;
                }
            }

            input = password.ToString();
            yield return input;
        }
    }
}