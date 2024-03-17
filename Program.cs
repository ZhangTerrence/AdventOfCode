using AdventOfCode.Lib;

const string usage =
    """
    Usage: dotnet run [YEAR [DAY...]...]
    Prints the puzzle solutions for the given year(s) and day(s).
    
        YEAR    If no year is given, prints every solution for every day. Must be in YYYY format.
        DAY     If no day is given, prints every solution for the given year. Must be separated by 
                commas if multiple are given.
    """;

var givenDays = new Dictionary<string, List<string>>();

for (var i = 0; i < args.Length;)
{
    if (args[i].Length == 4)
    {
        if (args[i + 1].Contains(',') || args[i + 1].Length <= 2)
        {
            var dayList = args[i + 1].Split(',').ToList();
            if (dayList.Any(day => day.Length > 2))
            {
                Console.WriteLine("Invalid day.");
                Console.WriteLine("------------");
                Console.WriteLine(usage);
                Environment.Exit(1);
            }
            
            givenDays[args[i]] = dayList;
            i += 2;
        }
        else
        {
            givenDays[args[i]] = [];
            i += 1;
        }
    }
    else
    {
        Console.WriteLine("Must start with a year in YYYY format.");
        Console.WriteLine("--------------------------------------");
        Console.WriteLine(usage);
        Environment.Exit(1);
    }
}

try
{
   Runner.RunSolutions(givenDays);
}
catch (Exception error)
{
    Console.WriteLine($"{error.GetType()}: {error.Message}");
    Environment.Exit(1);
}