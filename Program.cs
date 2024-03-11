using AdventOfCode.Lib;

const string usage =
    """
    Usage: dotnet run [YEAR [DAY...]]
    Prints the puzzle solutions for the given year(s) and day(s).
    
        YEAR    If no year is given, prints every solution for every day. Must be in YYYY format.
        DAY     If no day is given, prints every solution for the given year. Must be in DD format and separated
                by commas if multiple are given.
    """;

if (args.Length > 2)
{
    Console.WriteLine("Invalid number of arguments.");
    Console.WriteLine("----------------------------");
    Console.WriteLine(usage);
    Environment.Exit(1);
}

List<string> year = [];
List<string> days = [];

switch (args.Length)
{
    case 0:
        break;
    case 1:
        year = [args[0]];
        break;
    case 2:
        year = [args[0]];
        days = args[1].Split(",").ToList();
        break;
}

try
{
    Runner.RunSolutions(year, days);
}
catch (Exception error)
{
    Console.WriteLine($"{error.GetType()}: {error.Message}");
    Environment.Exit(1);
}