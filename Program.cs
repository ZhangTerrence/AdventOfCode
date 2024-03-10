using AdventOfCode.Lib;

const string usage =
    """
    Usage: dotnet run <year> <day>
    Prints the puzzle solution at the given year and day.
    """;

if (args.Length != 2)
{
    Console.WriteLine("Invalid number of arguments.");
    Console.WriteLine("----------------------------");
    Console.WriteLine(usage);
    Environment.Exit(1);
}

try
{
    string year = args[0], day = args[1];

    Runner.RunSolutions(year, day);
}
catch (Exception error)
{
    Console.WriteLine($"{error.GetType()}: {error.Message}");
    Environment.Exit(1);
}