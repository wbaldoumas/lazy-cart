using System.Numerics;

namespace LazyCart;

/// <summary>
///    Represents a Cartesian product of two sets.
/// </summary>
/// <typeparam name="T1">The type of the first set.</typeparam>
/// <typeparam name="T2">The type of the second set.</typeparam>
public interface ILazyCartesianProduct<T1, T2>
{
    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2) this[BigInteger index] { get; }

    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2) AtIndex(BigInteger index);

    /// <summary>
    ///    Returns the index of the given Cartesian product entry.
    /// </summary>
    /// <param name="entry">The Cartesian product entry to find the index of.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the Cartesian product entry is not found.</exception>
    /// <returns>The index of the given Cartesian product entry.</returns>
    BigInteger IndexOf((T1, T2) entry);
}