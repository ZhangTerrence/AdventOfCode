using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode._2015._07;

[Solution("Some Assembly Required", 2015, 07)]
public class Solution(string inputPath) : SolutionBase(inputPath)
{
    protected override int SolvePartOne()
    {
        return ParseInstructions()["a"](new Signals());
    }

    protected override int SolvePartTwo()
    {
        return ParseInstructions()["a"](new Signals { ["b"] = SolvePartOne() });
    }

    private SignalCalculator ParseInstructions()
    {
        return Input.Aggregate(new SignalCalculator(), (signalCalculator, instruction) =>
            ApplyGate(signalCalculator, instruction, @"(\w+) AND (\w+) -> (\w+)",
                operands => operands[0] & operands[1]) ??
            ApplyGate(signalCalculator, instruction, @"(\w+) OR (\w+) -> (\w+)",
                operands => operands[0] | operands[1]) ??
            ApplyGate(signalCalculator, instruction, @"(\w+) RSHIFT (\w+) -> (\w+)",
                operands => operands[0] >> operands[1]) ??
            ApplyGate(signalCalculator, instruction, @"(\w+) LSHIFT (\w+) -> (\w+)",
                operands => operands[0] << operands[1]) ??
            ApplyGate(signalCalculator, instruction, @"NOT (\w+) -> (\w+)", operands => ~operands[0]) ??
            ApplyGate(signalCalculator, instruction, @"(\w+) -> (\w+)", operands => operands[0]) ??
            throw new Exception(instruction));
    }

    private static SignalCalculator? ApplyGate(
        SignalCalculator? signalCalculator,
        string instruction,
        string pattern,
        Func<List<int>, int> bitOp
    )
    {
        if (signalCalculator == null) return null;

        var match = Regex.Match(instruction, pattern);
        if (!match.Success) return null;

        var tokens = match.Groups.Cast<Group>().Skip(1).Select(e => e.Value).ToList();

        var circuit = tokens.Last();
        var operands = tokens[..^1];

        signalCalculator[circuit] = state =>
        {
            if (state.TryGetValue(circuit, out var signal)) return signal;

            var intOperands = operands
                .Select(operand => int.TryParse(operand, out var i) ? i : signalCalculator[operand](state)).ToList();
            state[circuit] = bitOp(intOperands);

            return state[circuit];
        };

        return signalCalculator;
    }

    private class SignalCalculator : Dictionary<string, Func<Signals, int>>;

    private class Signals : Dictionary<string, int>;
}