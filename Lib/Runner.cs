using System.Reflection;

namespace AdventOfCode.Lib;

public static class Runner
{
    public static void RunSolutions(Dictionary<string, List<string>> givenDays)
    {
        var givenIntDays = ValidateInputs(givenDays);
        var solutions = GetSolutions(givenIntDays);

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

    private static Dictionary<int, List<int>> ValidateInputs(Dictionary<string, List<string>> givenDays)
    {
        var givenIntDays = new Dictionary<int, List<int>>();

        foreach (var year in givenDays.Keys)
        {
            if (!int.TryParse(year, out var intYear))
                throw new Exception("Invalid year.");

            givenIntDays[intYear] = [];

            foreach (var day in givenDays[year])
            {
                if (!int.TryParse(day, out var intDay))
                    throw new Exception("Invalid day.");

                givenIntDays[intYear].Add(intDay);
            }
        }

        return givenIntDays;
    }

    private static List<(SolutionBase solution, SolutionAttribute attr)> GetSolutions(
        Dictionary<int, List<int>> givenIntDays)
    {
        var assemblies = Assembly.GetExecutingAssembly().GetTypes()
            .Where(e => e.IsSubclassOf(typeof(SolutionBase))).ToList();

        var allSolutions = new List<(SolutionBase solution, SolutionAttribute attr)>();

        foreach (var assembly in assemblies)
        {
            var attr = assembly.GetCustomAttribute<SolutionAttribute>();

            if (attr is null) continue;

            var inputPath = Path.Combine([Environment.CurrentDirectory, $"{attr.Year}", $"{attr.Day:00}", "input.txt"]);
            var instance = Activator.CreateInstance(assembly, inputPath);

            if (instance is null) continue;

            allSolutions.Add(((SolutionBase)instance, attr));
        }

        var solutions = new List<(SolutionBase solution, SolutionAttribute attr)>();

        if (givenIntDays.Count == 0) return allSolutions;

        foreach (var year in givenIntDays.Keys)
        {
            if (givenIntDays[year].Count == 0)
            {
                solutions.AddRange(allSolutions.Where(solution => solution.attr.Year == year));
                continue;
            }

            foreach (var day in givenIntDays[year])
                solutions.AddRange(allSolutions
                    .Where(solution => solution.attr.Year == year && solution.attr.Day == day).ToList());
        }

        return solutions;
    }
}