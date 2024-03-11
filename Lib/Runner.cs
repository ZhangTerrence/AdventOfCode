using System.Reflection;

namespace AdventOfCode.Lib;

public static class Runner
{
    public static void RunSolutions(List<string> year, List<string> days)
    {
        var (intYears, intDays) = ValidateInputs(year, days);
        var solutions = GetSolutions(intYears, intDays);

        if (solutions.Count == 0)
        {
            Console.WriteLine("No solutions found.");
        }
        else
        {
            var groupedSolutions = solutions.OrderBy(e => e.attr.Year + e.attr.Day).GroupBy(e => e.attr.Year).ToList();

            foreach (var group in groupedSolutions)
            {
                var groupYear = group.FirstOrDefault().attr.Year;
                var i = 0;

                Console.WriteLine($"{groupYear}");

                foreach (var pair in group)
                {
                    var dayAscii = i == group.Count() - 1
                        ? $"\u2514\u2500\u2500 {pair.attr.Day:00}. {pair.attr.Title}:"
                        : $"\u251c\u2500\u2500 {pair.attr.Day:00}. {pair.attr.Title}:";
                    Console.WriteLine(dayAscii);

                    var solutionAscii1 = i == group.Count() - 1
                        ? "   \u251c\u2500\u2500"
                        : "\u2502  \u251c\u2500\u2500";
                    var solutionAscii2 = i == group.Count() - 1
                        ? "   \u2514\u2500\u2500"
                        : "\u2502  \u2514\u2500\u2500";
                    pair.solution.PrintSolutions(solutionAscii1, solutionAscii2);

                    i++;
                }
            }
        }
    }

    private static (List<int>, List<int>) ValidateInputs(List<string> year, List<string> days)
    {
        var intYears = new List<int>();

        foreach (var e in year)
        {
            if (!int.TryParse(e, out var intYear))
                throw new ArgumentException("Invalid argument type.");

            if (e.Length != 4) throw new ArgumentException("Invalid argument length.");

            intYears.Add(intYear);
        }

        var intDays = new List<int>();

        foreach (var e in days)
        {
            if (!int.TryParse(e, out var intDay))
                throw new ArgumentException("Invalid argument type.");

            intDays.Add(intDay);
        }

        return (intYears, intDays);
    }

    private static List<(SolutionBase solution, SolutionAttribute attr)> GetSolutions(List<int> year, List<int> days)
    {
        var assemblies = Assembly.GetExecutingAssembly().GetTypes()
            .Where(e => e.IsSubclassOf(typeof(SolutionBase))).ToList();

        var allSolutions =
            new List<(SolutionBase solution, SolutionAttribute attr)>();

        foreach (var assembly in assemblies)
        {
            var attr = assembly.GetCustomAttribute<SolutionAttribute>();

            if (attr is null) continue;

            var instance =
                Activator.CreateInstance(assembly,
                    $"./{attr.Year}/{attr.Day:00}/input.txt");

            if (instance is null) continue;

            allSolutions.Add(((SolutionBase)instance, attr));
        }

        switch (year.Count)
        {
            case 0 when days.Count == 0:
                return allSolutions;
            case 1 when days.Count == 0:
                return allSolutions.Where(e => e.attr.Year == year[0]).ToList();
            case 1 when days.Count > 0:
                var specificSolutions = new List<(SolutionBase solution, SolutionAttribute attr)>();

                foreach (var day in days)
                    specificSolutions.AddRange(allSolutions.Where(e => e.attr.Year == year[0] && e.attr.Day == day)
                        .ToList());

                return specificSolutions;
            default:
                return [];
        }
    }
}