using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._04;

[Solution("The Ideal Stocking Stuffer", 2015, 04)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return ComputeLowest("00000");
    }

    protected override int SolvePartTwo()
    {
        return ComputeLowest("000000");
    }

    private int ComputeLowest(string match)
    {
        var queue = new ConcurrentQueue<int>();

        Parallel.ForEach(
            Integers(),
            MD5.Create,
            (i, state, md5) =>
            {
                var inputBytes = Encoding.ASCII.GetBytes($"{Input[0]}{i}");
                var hashBytes = MD5.HashData(inputBytes);
                var hashString = Convert.ToHexString(hashBytes);

                if (!hashString.StartsWith(match)) return md5;

                queue.Enqueue(i);
                state.Stop();
                return md5;
            },
            _ => { }
        );

        return queue.Min();
    }

    private static IEnumerable<int> Integers()
    {
        for (var i = 0;; i++) yield return i;
    }
}