using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._04;

[Solution("The Ideal Stocking Stuffer", 2015, 04)]
public class Solution(string inputPath) : SolutionBase
{
    private string InputPath { get; } = inputPath;
    private string? Input { get; set; }

    public override void PrintSolutions(string ascii1, string ascii2)
    {
        Input = File.ReadAllText(InputPath);

        var number1 = SolvePartOne();
        Console.WriteLine($"{ascii1} Part 1 => {number1}");

        var number2 = SolvePartTwo();
        Console.WriteLine($"{ascii2} Part 2 => {number2}");
    }

    private int SolvePartOne()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        var i = 0;
        var inputBytes = Encoding.ASCII.GetBytes($"{input}{i}");
        var hashBytes = MD5.HashData(inputBytes);
        var hashString = Convert.ToHexString(hashBytes);
        while (hashString[..5] != "00000")
        {
            i++;
            inputBytes = Encoding.ASCII.GetBytes($"{input}{i}");
            hashBytes = MD5.HashData(inputBytes);
            hashString = Convert.ToHexString(hashBytes);
        }

        return i;
    }

    private int SolvePartTwo()
    {
        var input = Input ?? throw new NullReferenceException("Input is null.");

        var i = 0;
        var inputBytes = Encoding.ASCII.GetBytes($"{input}{i}");
        var hashBytes = MD5.HashData(inputBytes);
        var hashString = Convert.ToHexString(hashBytes);
        while (hashString[..6] != "000000")
        {
            i++;
            inputBytes = Encoding.ASCII.GetBytes($"{input}{i}");
            hashBytes = MD5.HashData(inputBytes);
            hashString = Convert.ToHexString(hashBytes);
        }

        return i;
    }
}