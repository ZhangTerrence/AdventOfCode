using System.Reflection;

namespace AdventOfCode.Lib;

public static class Runner
{
    public static void RunSolutions(string year, string day)
    {
        var (intYear, intDay) = ValidateInput(year, day);
        var solutions = GetSolutions(intYear, intDay);

        if (solutions.Count == 0)
            Console.WriteLine("No solutions found.");
        else
            foreach (var pair in solutions)
            {
                Console.WriteLine($"{pair.attr.Title}:");
                pair.solution.PrintSolutions();
            }
    }

    private static (int, int) ValidateInput(string x, string y)
    {
        if (!int.TryParse(x, out var year) || !int.TryParse(y, out var day))
            throw new ArgumentException("Invalid argument type.");

        return (year, day);
    }

    private static List<(SolutionBase solution, SolutionAttribute attr)>
        GetSolutions(
            int year, int day)
    {
        var assemblies = Assembly.GetExecutingAssembly().GetTypes()
            .Where(e => e.IsSubclassOf(typeof(SolutionBase)));

        var allSolutions =
            new List<(SolutionBase solution, SolutionAttribute attr)>();

        foreach (var assembly in assemblies)
        {
            var instance =
                Activator.CreateInstance(assembly,
                    $"./{year}/{day:00}/input.txt");
            var attr = assembly.GetCustomAttribute<SolutionAttribute>();

            if (instance is not null && attr is not null)
                allSolutions.Add(((SolutionBase)instance, attr));
        }

        var solutions =
            allSolutions.Where(e => e.attr.Year == year && e.attr.Day == day);

        return solutions.ToList();
    }
}