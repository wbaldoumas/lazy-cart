using System.Numerics;

namespace LazyCart;

/// <summary>
///    Represents a Cartesian product of sets.
/// </summary>
public interface ILazyCartesianProduct
{
    /// <summary>
    ///     Gets the size of the Cartesian product.
    /// </summary>
    BigInteger Size { get; }
}

/// <summary>
///    Represents a Cartesian product of two sets.
/// </summary>
/// <typeparam name="T1">The type of the first set.</typeparam>
/// <typeparam name="T2">The type of the second set.</typeparam>
public interface ILazyCartesianProduct<T1, T2> : ILazyCartesianProduct
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

    /// <summary>
    ///     Generates an evenly-distributed, random sample of Cartesian product entries.
    /// </summary>
    /// <param name="sampleSize">The number of samples to generate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="sampleSize"/> is greater than <see cref="ILazyCartesianProduct.Size"/>.</exception>
    /// <returns>An evenly-distributed, random sample of Cartesian product entries.</returns>
    IEnumerable<(T1, T2)> GenerateSamples(BigInteger sampleSize);
}

/// <summary>
///    Represents a Cartesian product of three sets.
/// </summary>
/// <typeparam name="T1">The type of the first set.</typeparam>
/// <typeparam name="T2">The type of the second set.</typeparam>
/// <typeparam name="T3">The type of the third set.</typeparam>
public interface ILazyCartesianProduct<T1, T2, T3> : ILazyCartesianProduct
{
    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2, T3) this[BigInteger index] { get; }

    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2, T3) AtIndex(BigInteger index);

    /// <summary>
    ///    Returns the index of the given Cartesian product entry.
    /// </summary>
    /// <param name="entry">The Cartesian product entry to find the index of.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the Cartesian product entry is not found.</exception>
    /// <returns>The index of the given Cartesian product entry.</returns>
    BigInteger IndexOf((T1, T2, T3) entry);

    /// <summary>
    ///     Generates an evenly-distributed, random sample of Cartesian product entries.
    /// </summary>
    /// <param name="sampleSize">The number of samples to generate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="sampleSize"/> is greater than <see cref="ILazyCartesianProduct.Size"/>.</exception>
    /// <returns>An evenly-distributed, random sample of Cartesian product entries.</returns>
    IEnumerable<(T1, T2, T3)> GenerateSamples(BigInteger sampleSize);
}

/// <summary>
///    Represents a Cartesian product of four sets.
/// </summary>
/// <typeparam name="T1">The type of the first set.</typeparam>
/// <typeparam name="T2">The type of the second set.</typeparam>
/// <typeparam name="T3">The type of the third set.</typeparam>
/// <typeparam name="T4">The type of the fourth set.</typeparam>
public interface ILazyCartesianProduct<T1, T2, T3, T4> : ILazyCartesianProduct
{
    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2, T3, T4) this[BigInteger index] { get; }

    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2, T3, T4) AtIndex(BigInteger index);

    /// <summary>
    ///    Returns the index of the given Cartesian product entry.
    /// </summary>
    /// <param name="entry">The Cartesian product entry to find the index of.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the Cartesian product entry is not found.</exception>
    /// <returns>The index of the given Cartesian product entry.</returns>
    BigInteger IndexOf((T1, T2, T3, T4) entry);

    /// <summary>
    ///     Generates an evenly-distributed, random sample of Cartesian product entries.
    /// </summary>
    /// <param name="sampleSize">The number of samples to generate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="sampleSize"/> is greater than <see cref="ILazyCartesianProduct.Size"/>.</exception>
    /// <returns>An evenly-distributed, random sample of Cartesian product entries.</returns>
    IEnumerable<(T1, T2, T3, T4)> GenerateSamples(BigInteger sampleSize);
}

/// <summary>
///    Represents a Cartesian product of five sets.
/// </summary>
/// <typeparam name="T1">The type of the first set.</typeparam>
/// <typeparam name="T2">The type of the second set.</typeparam>
/// <typeparam name="T3">The type of the third set.</typeparam>
/// <typeparam name="T4">The type of the fourth set.</typeparam>
/// <typeparam name="T5">The type of the fifth set.</typeparam>
public interface ILazyCartesianProduct<T1, T2, T3, T4, T5> : ILazyCartesianProduct
{
    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2, T3, T4, T5) this[BigInteger index] { get; }

    /// <summary>
    ///     Returns the Cartesian product entry at the given index.
    /// </summary>
    /// <param name="index">The index of the Cartesian product entry to return.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    /// <returns>The Cartesian product entry at the given index.</returns>
    (T1, T2, T3, T4, T5) AtIndex(BigInteger index);

    /// <summary>
    ///    Returns the index of the given Cartesian product entry.
    /// </summary>
    /// <param name="entry">The Cartesian product entry to find the index of.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the Cartesian product entry is not found.</exception>
    /// <returns>The index of the given Cartesian product entry.</returns>
    BigInteger IndexOf((T1, T2, T3, T4, T5) entry);

    /// <summary>
    ///     Generates an evenly-distributed, random sample of Cartesian product entries.
    /// </summary>
    /// <param name="sampleSize">The number of samples to generate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="sampleSize"/> is greater than <see cref="ILazyCartesianProduct.Size"/>.</exception>
    /// <returns>An evenly-distributed, random sample of Cartesian product entries.</returns>
    IEnumerable<(T1, T2, T3, T4, T5)> GenerateSamples(BigInteger sampleSize);
}