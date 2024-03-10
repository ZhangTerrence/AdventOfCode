namespace AdventOfCode.Lib;

public class SolutionAttribute(string title, int year, int day) : Attribute
{
    public string Title { get; } = title;
    public int Year { get; } = year;
    public int Day { get; } = day;
}