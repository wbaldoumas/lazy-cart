using System.Numerics;

namespace LazyCart;

/// <summary>
///     Provides a thread-safe way to generate random numbers.
/// </summary>
internal static class ThreadLocalRandom
{
    private static readonly ThreadLocal<Random> ThreadLocalRandomStorage = new(
        () => new Random(Interlocked.Increment(ref _seed))
    );

    private static int _seed = Environment.TickCount;

    /// <summary>
    ///    Gets the thread-local random number generator.
    /// </summary>
    public static Random Instance => ThreadLocalRandomStorage.Value!;

    /// <summary>
    ///     Generates a random BigInteger within the specified range.
    /// </summary>
    /// <param name="from">The lower bound of the range.</param>
    /// <param name="to">The upper bound of the range.</param>
    /// <returns>A random BigInteger within the specified range.</returns>
    public static BigInteger NextBigInteger(BigInteger from, BigInteger to)
    {
        var bytes = to.ToByteArray();

        // Generate a random number from zero to to
        Instance.NextBytes(bytes);
        bytes[^1] &= 0x7F; // force sign bit to positive
        var result = new BigInteger(bytes);

        // Ensure result is in [from, to) interval
        return from + result % (to - from);
    }
}