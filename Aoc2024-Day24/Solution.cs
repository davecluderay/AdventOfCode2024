using System.Diagnostics;

namespace Aoc2024_Day24;

internal class Solution
{
    public string Title => "Day 24: Crossed Wires";

    public object PartOne()
    {
        var (wireStates, gates) = ReadInput();
        var z = ReadWireGroup('z', wireStates, gates);
        return z;
    }

    public object PartTwo()
    {
        var (wireStates, gates) = ReadInput();
        HashSet<string> badOutputWires = new(); 

        // Check the wiring of each adder unit (see adder_structure.svg).
        var ubound = wireStates.Keys.Where(w => w.StartsWith('x')).Max(w => int.Parse(w.TrimStart('x')));
        for (var n = 1; n <= ubound; n++)
        {
            var (x, y, z) = ($"x{n:D2}", $"y{n:D2}", $"z{n:D2}");

            // Report if the x wire is connected as an output.
            if (gates.FindByOutputWire(x) is not null)
                badOutputWires.Add(x);

            // Report if the y wire is connected as an output.
            if (gates.FindByOutputWire(y) is not null)
                badOutputWires.Add(y);

            // Ensure that the x and y wires both feed into an AND gate.
            Gate and = gates.FindByInputWires(x, y).SingleOrDefault(g => g.Op == "AND")
                       ?? throw new Exception($"{x} AND {y} gate not found");

            // Report if the AND gate's output wire feeds into more than one gate.
            if (gates.FindByOneInputWire(and.OutputWire).Length > 1)
                badOutputWires.Add(and.OutputWire);

            // Report if the AND gate's output wire doesn't feed into an OR gate.
            var or = gates.FindByOneInputWire(and.OutputWire).SingleOrDefault(g => g.Op == "OR");
            if (or is null)
                badOutputWires.Add(and.OutputWire);

            // Ensure that the x and y wires both feed into an XOR gate.
            Gate xor = gates.FindByInputWires(x, y).SingleOrDefault(g => g.Op == "XOR")
                       ?? throw new Exception($"{x} XOR {y} gate not found");

            // Report if the XOR gate's output wire feeds into more than two gates.
            if (gates.FindByOneInputWire(xor.OutputWire).Length > 2)
                badOutputWires.Add(xor.OutputWire);

            // Report if the XOR gate's output wire doesn't feed into another XOR gate.
            var xor2 = gates.FindByOneInputWire(xor.OutputWire).SingleOrDefault(g => g.Op == "XOR");
            if (xor2 is null)
                badOutputWires.Add(xor.OutputWire);

            // Report if the second XOR gate doesn't output to the z wire.
            if (xor2 is not null && xor2.OutputWire != z)
                badOutputWires.Add(xor2.OutputWire);

            // Report if the XOR gate's output wire doesn't feed into another AND gate.
            var and2 = gates.FindByOneInputWire(xor.OutputWire).SingleOrDefault(g => g.Op == "AND");
            if (and2 is null)
                badOutputWires.Add(xor.OutputWire);

            // Report if the second AND gate's output wire doesn't feed into the previously found OR gate.
            if (and2 is not null && or is not null && or.InputWire1 != and2.OutputWire && or.InputWire2 != and2.OutputWire)
                badOutputWires.Add(and2.OutputWire);

            // Report if the z wire is the output from a gate that isn't an XOR gate.
            if (!gates.TryGetValue(z, out var v) || v.Op != "XOR")
                badOutputWires.Add(z);
        }

        return string.Join(',', badOutputWires.Order());
    }

    private static long ReadWireGroup(char prefix, Dictionary<string, int> wireStates, Dictionary<string, Gate> gates)
    {
        var outputWires = gates.Keys
                               .Where(k => k.StartsWith(prefix))
                               .Concat(wireStates.Keys.Where(k => k.StartsWith(prefix)))
                               .OrderDescending()
                               .ToArray();
        var result = 0L;
        foreach (var outputWire in outputWires)
        {
            result = (result << 1) | GetOutput(outputWire, wireStates, gates);
        }
        return result;

        static long GetOutput(string outputWire, Dictionary<string, int> wireStates, Dictionary<string, Gate> gates)
        {
            if (wireStates.TryGetValue(outputWire, out var value)) return value;
            var gate = gates[outputWire];
            var (input1, input2) = (GetOutput(gate.InputWire1, wireStates, gates),
                                    GetOutput(gate.InputWire2, wireStates, gates));
            return gate.Op switch
                   {
                       "AND" => input1 & input2,
                       "OR"  => input1 | input2,
                       "XOR" => input1 ^ input2,
                       _     => throw new UnreachableException()
                   };
        }
    }

    private static (Dictionary<string, int> InputWireStates, Dictionary<string, Gate> Gates) ReadInput()
    {
        var sections = InputFile.ReadInSections().ToArray();
        var wireStates = sections[0].Select(l => l.Split(':', StringSplitOptions.TrimEntries))
                                    .ToDictionary(s => s[0], s => int.Parse(s[1]));
        var gates = sections[1].Select(Gate.Parse)
                               .ToDictionary(g => g.OutputWire);
        return (wireStates, gates);
    }
}
