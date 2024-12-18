namespace Aoc2024_Day18;

internal record Parameters(Vector[] CorruptionSequence, int DimensionLength)
{
    public Vector Start => (0, 0);
    public Vector End => (DimensionLength - 1, DimensionLength - 1);

    public static Parameters Read()
    {
        Vector[] sequence = InputFile.ReadAllLines()
                                     .Select(l => l.Split(','))
                                     .Select(a => new Vector(X: int.Parse(a[0]),
                                                             Y: int.Parse(a[1])))
                                     .ToArray();
        var dimensionLength = sequence.Any(b => b.X > 6 || b.Y > 6)
                                  ? 71
                                  : 7;
        return new(sequence, dimensionLength);
    }
}
