namespace Aoc2024_Day07;

internal static class Operator
{
    public static long Add(long a, long b)
        => a + b;

    public static long Mul(long a, long b)
        => a * b;

    public static long Cat(long a, long b)
    {
        return a * Multiplier(b) + b;

        static long Multiplier(long number)
            => number switch
               {
                   < 10                  => 10,
                   < 100                 => 100,
                   < 1000                => 1000,
                   < 10000               => 10000,
                   < 100000              => 100000,
                   < 1000000             => 1000000,
                   < 10000000            => 10000000,
                   < 100000000           => 100000000,
                   < 1000000000          => 1000000000,
                   < 10000000000         => 10000000000,
                   < 100000000000        => 100000000000,
                   < 1000000000000       => 1000000000000,
                   < 10000000000000      => 10000000000000,
                   < 100000000000000     => 100000000000000,
                   < 1000000000000000    => 1000000000000000,
                   < 10000000000000000   => 10000000000000000,
                   < 100000000000000000  => 100000000000000000,
                   < 1000000000000000000 => 1000000000000000000,
                   _                     => throw new ArgumentOutOfRangeException(nameof(number))
               };
    }
}
