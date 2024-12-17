using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024_Day17;

internal class Computer
{
    private readonly int[] _program;
    private readonly List<int> _output = new();

    private long _registerA;
    private long _registerB;
    private long _registerC;
    private int _instructionPointer;

    private Computer(int registerA, int registerB, int registerC, int[] program)
        => (_registerA, _registerB, _registerC, _program) = (registerA, registerB, registerC, program);

    public int[] RunProgram(bool stopAtJump = false)
    {
        while (_instructionPointer >= 0 && _instructionPointer < _program.Length)
        {
            var opcode = _program[_instructionPointer];

            if (stopAtJump && opcode == Opcodes.Jnz) break;

            switch (opcode)
            {
                case Opcodes.Adv: Adv(); break;
                case Opcodes.Bxl: Bxl(); break;
                case Opcodes.Bst: Bst(); break;
                case Opcodes.Jnz: Jnz(); break;
                case Opcodes.Bxc: Bxc(); break;
                case Opcodes.Out: Out(); break;
                case Opcodes.Bdv: Bdv(); break;
                case Opcodes.Cdv: Cdv(); break;
                default: throw new InvalidOperationException($"Invalid opcode: {opcode}");
            }
        }
        return _output.ToArray();
    }

    public long FindRegisterValueForProgramToOutputItself()
    {
        // Program analysis:
        //   Loop while A != 0
        //     Set B to the lowest three bits of A
        //     ...transform B about a bit...
        //     Output the lowest three bits of B
        //     A = A / 8
        //   Loop
        // Approach:
        //   Each iteration through the loop, A is divided by eight.
        //   After the last iteration, it is zero (hence why the program ends).
        //   Work backwards, simulating the iterations in reverse:
        //     - Find the starting value of A that produces the correct output value.
        //     - Limit the starting values of A to those that, when divided by eight, produce the right result for the iteration that follows.

        return FindCore(requiredOutputs: _program.Reverse().ToArray(),
                        requiredA: 0);

        long FindCore(IEnumerable<int> requiredOutputs, long requiredA)
        {
            var outputs = requiredOutputs.ToList();
            if (outputs.Count == 0) return requiredA;

            var desiredOutput = outputs.First();

            for (var startingA = requiredA * 8; startingA < (requiredA + 1) * 8; startingA++)
            {
                Reset(registerA: startingA);
                var output = RunProgram(stopAtJump: true).Single();
                if (output != desiredOutput) continue;

                var nextRequiredA = FindCore(outputs.Skip(1), startingA);
                if (nextRequiredA != -1) return nextRequiredA;
            }

            return -1;
        }
    }

    private void Reset(long registerA = 0)
    {
        _registerA = registerA;
        _registerB = 0;
        _registerC = 0;
        _instructionPointer = 0;
        _output.Clear();
    }

    #region Instructions
    private void Adv()
    {
        var operand = ReadComboOperand();
        var denominator = 1 << (int)operand;
        _registerA /= denominator;
        _instructionPointer += 2;
    }

    private void Bxl()
    {
        var operand = ReadLiteralOperand();
        _registerB ^= operand;
        _instructionPointer += 2;
    }

    private void Bst()
    {
        var operand = ReadComboOperand();
        _registerB = operand & 7;
        _instructionPointer += 2;
    }

    private void Jnz()
    {
        _instructionPointer = _registerA == 0
                                  ? _instructionPointer + 2
                                  : ReadLiteralOperand();
    }

    private void Bxc()
    {
        _registerB ^= _registerC;
        _instructionPointer += 2;
    }

    private void Out()
    {
        var operand = ReadComboOperand();
        Output((int)(operand & 7));
        _instructionPointer += 2;
    }

    private void Bdv()
    {
        var operand = ReadComboOperand();
        var denominator = 1 << (int)operand;
        _registerB = _registerA / denominator;
        _instructionPointer += 2;
    }

    private void Cdv()
    {
        var operand = ReadComboOperand();
        var denominator = 1 << (int)operand;
        _registerC = _registerA / denominator;
        _instructionPointer += 2;
    }

    private int ReadLiteralOperand()
    {
        return _program[_instructionPointer + 1];
    }

    private long ReadComboOperand()
    {
        var operand = _program[_instructionPointer + 1];
        return operand switch
               {
                   4 => _registerA,
                   5 => _registerB,
                   6 => _registerC,
                   7 => throw new UnreachableException(),
                   _ => operand
               };
    }

    private void Output(int value)
    {
        _output.Add(value);
    }
    #endregion

    public static Computer Read()
    {
        var text = InputFile.ReadAllText();
        var pattern = new Regex(@"^Register A:\s+(?<A>\d+)\s+" +
                                @"Register B:\s+(?<B>\d+)\s+" +
                                @"Register C:\s+(?<C>\d+)\s+" +
                                @"Program:\s+(?<Instruction>[0-7])(,(?<Instruction>[0-7]))*$",
                                RegexOptions.Singleline | RegexOptions.ExplicitCapture);
        var match = pattern.Match(text);
        if (!match.Success) throw new FormatException("Invalid input format.");

        var registerA = int.Parse(match.Groups["A"].Value);
        var registerB = int.Parse(match.Groups["B"].Value);
        var registerC = int.Parse(match.Groups["C"].Value);
        var program = match.Groups["Instruction"].Captures.Select(c => int.Parse(c.Value)).ToArray();

        return new Computer(registerA, registerB, registerC, program);
    }

    private static class Opcodes
    {
        public const int Adv = 0;
        public const int Bxl = 1;
        public const int Bst = 2;
        public const int Jnz = 3;
        public const int Bxc = 4;
        public const int Out = 5;
        public const int Bdv = 6;
        public const int Cdv = 7;
    }
}
