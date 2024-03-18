using System.Text.Json;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._12;

[Solution("JSAbacusFramework.io", 2015, 12)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override object SolvePartOne()
    {
        return ParseJson(JsonDocument.Parse(Input[0]).RootElement, false);
    }

    protected override object SolvePartTwo()
    {
        return ParseJson(JsonDocument.Parse(Input[0]).RootElement, true);
    }

    private static int ParseJson(JsonElement jsonString, bool skipRed)
    {
        return jsonString.ValueKind switch
        {
            JsonValueKind.Object when skipRed && jsonString.EnumerateObject().Any(o =>
                o.Value.ValueKind == JsonValueKind.String && o.Value.ToString() == "red") => 0,
            JsonValueKind.Object => jsonString.EnumerateObject().Select(o => ParseJson(o.Value, skipRed)).Sum(),
            JsonValueKind.Array => jsonString.EnumerateArray().Select(o => ParseJson(o, skipRed)).Sum(),
            JsonValueKind.Number => jsonString.GetInt32(),
            _ => 0
        };
    }
}