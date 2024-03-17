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
            
            Console.WriteLine("Advent Of Code");

            foreach (var group in groupedSolutions)
            {
                var groupYear = group.FirstOrDefault().attr.Year;

                var lastYear = group == groupedSolutions.Last();
                var yearAscii = lastYear
                    ? $"\u2514\u2500\u2500 {groupYear}"
                    : $"\u251c\u2500\u2500 {groupYear}";
                Console.WriteLine(yearAscii);

                var mainLine = lastYear ? " " : "\u2502";
                foreach (var pair in group)
                {
                    var lastDay = pair == group.Last();
                    var yearLine = lastDay ? " " : "\u2502";
                    
                    var dayAscii = lastDay
                        ? $"{mainLine}   \u2514\u2500\u2500 {pair.attr.Day:00}. {pair.attr.Title}:"
                        : $"{mainLine}   \u251c\u2500\u2500 {pair.attr.Day:00}. {pair.attr.Title}:";
                    Console.WriteLine(dayAscii);

                    var topSolutionAscii = lastDay
                        ? $"{mainLine}       \u251c\u2500\u2500"
                        : $"{mainLine}   {yearLine}   \u251c\u2500\u2500";
                    var botSolutionAscii = lastDay
                        ? $"{mainLine}       \u2514\u2500\u2500"
                        : $"{mainLine}   {yearLine}   \u2514\u2500\u2500";
                    pair.solution.PrintSolutions(topSolutionAscii, botSolutionAscii);
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
                throw new Exception("Invalid argument type.");

            if (e.Length != 4) throw new Exception("Invalid argument length.");

            intYears.Add(intYear);
        }

        var intDays = new List<int>();

        foreach (var e in days)
        {
            if (!int.TryParse(e, out var intDay))
                throw new Exception("Invalid argument type.");

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

            var inputPath = Path.Combine([Environment.CurrentDirectory, $"{attr.Year}", $"{attr.Day:00}", "input.txt"]);
            var instance = Activator.CreateInstance(assembly, inputPath);

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