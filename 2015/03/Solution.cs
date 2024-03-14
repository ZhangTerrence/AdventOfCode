using AdventOfCode.Lib;

namespace AdventOfCode._2015._03;

[Solution("Perfectly Spherical Houses in a Vacuum", 2015, 03)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return DeliverPresents(1);
    }

    protected override int SolvePartTwo()
    {
        return DeliverPresents(2);
    }

    private int DeliverPresents(int deliverers)
    {
        var delivered = new HashSet<(int, int)> { (0, 0) };
        var positions = new (int x, int y)[deliverers];
        for (var i = 0; i < deliverers; i++) positions[i] = (0, 0);

        var deliverer = 0;
        foreach (var direction in Input[0])
        {
            switch (direction)
            {
                case '^':
                    positions[deliverer].y++;
                    break;
                case '>':
                    positions[deliverer].x++;
                    break;
                case 'v':
                    positions[deliverer].y--;
                    break;
                case '<':
                    positions[deliverer].x--;
                    break;
            }

            delivered.Add(positions[deliverer]);
            deliverer = (deliverer + 1) % deliverers;
        }

        return delivered.Count;
    }
}