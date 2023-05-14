﻿using System.Numerics;
using System.Runtime.CompilerServices;

namespace LazyCart;

public abstract class LazyCartesianProduct : ILazyCartesianProduct
{
    public BigInteger Size { get; protected set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static T CalculateItem<T>(BigInteger index, BigInteger dividend, BigInteger modulus, IList<T> set)
    {
        dividend = index / dividend;
        modulus = dividend % modulus;

        return set[(int)modulus];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void ValidateSampleSize(BigInteger sampleSize, BigInteger size)
    {
        if (sampleSize > size)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sampleSize),
                "Sample size cannot be greater than the total number of possible combinations"
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void ValidateAtIndex(BigInteger index, BigInteger size)
    {
        if (index < BigInteger.Zero || index >= size)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void ValidateIndexOf(string? entry, params int[] indexes)
    {
        if (indexes.Any(index => index < 0))
        {
            throw new ArgumentOutOfRangeException(nameof(entry), "Could not find Cartesian product entry");
        }
    }
}

/// <inheritdoc cref="ILazyCartesianProduct{T1,T2}"/>
public sealed class LazyCartesianProduct<T1, T2> : LazyCartesianProduct, ILazyCartesianProduct<T1, T2>
{
    private readonly IList<T1> _set1;
    private readonly IList<T2> _set2;
    private readonly IList<BigInteger> _dividends;
    private readonly IList<BigInteger> _moduli;

    public LazyCartesianProduct(IList<T1> set1, IList<T2> set2)
    {
        _set1 = set1;
        _set2 = set2;
        _dividends = new List<BigInteger>();
        _moduli = new List<BigInteger>();
        Size = BigInteger.One;

        Precompute();
    }

    public (T1, T2) this[BigInteger index] => AtIndex(index);

    public (T1, T2) AtIndex(BigInteger index)
    {
        ValidateAtIndex(index, Size);

        var item1 = CalculateItem(index, _dividends[0], _moduli[0], _set1);
        var item2 = CalculateItem(index, _dividends[1], _moduli[1], _set2);

        return (item1, item2);
    }

    public BigInteger IndexOf((T1, T2) entry)
    {
        var index1 = _set1.IndexOf(entry.Item1);
        var index2 = _set2.IndexOf(entry.Item2);

        ValidateIndexOf(nameof(entry), index1, index2);

        return index1 * _dividends[0] + index2 * _dividends[1];
    }

    public IEnumerable<(T1, T2)> GenerateSamples(BigInteger sampleSize)
    {
        ValidateSampleSize(sampleSize, Size);

        return GenerateSamplesInternal(sampleSize);
    }

    private IEnumerable<(T1, T2)> GenerateSamplesInternal(BigInteger sampleSize)
    {
        var sampledIndices = new HashSet<BigInteger>();

        while (sampledIndices.Count < (int)sampleSize)
        {
            var randomIndex = ThreadLocalRandom.NextBigInteger(0, Size);

            if (sampledIndices.Add(randomIndex))
            {
                yield return this[randomIndex];
            }
        }
    }

    private void Precompute()
    {
        Size *= _set1.Count;
        Size *= _set2.Count;

        var factor = BigInteger.One;

        _dividends.Insert(0, factor);
        _moduli.Insert(0, new BigInteger(_set2.Count));

        factor *= _set2.Count;

        _dividends.Insert(0, factor);
        _moduli.Insert(0, new BigInteger(_set1.Count));
    }
}

/// <inheritdoc cref="ILazyCartesianProduct{T1,T2,T3}"/>
public sealed class LazyCartesianProduct<T1, T2, T3> : LazyCartesianProduct, ILazyCartesianProduct<T1, T2, T3>
{
    private readonly IList<T1> _set1;
    private readonly IList<T2> _set2;
    private readonly IList<T3> _set3;
    private readonly IList<BigInteger> _dividends;
    private readonly IList<BigInteger> _moduli;

    public LazyCartesianProduct(IList<T1> set1, IList<T2> set2, IList<T3> set3)
    {
        _set1 = set1;
        _set2 = set2;
        _set3 = set3;
        _dividends = new List<BigInteger>();
        _moduli = new List<BigInteger>();
        Size = BigInteger.One;

        Precompute();
    }

    public (T1, T2, T3) this[BigInteger index] => AtIndex(index);

    public (T1, T2, T3) AtIndex(BigInteger index)
    {
        ValidateAtIndex(index, Size);

        var item1 = CalculateItem(index, _dividends[0], _moduli[0], _set1);
        var item2 = CalculateItem(index, _dividends[1], _moduli[1], _set2);
        var item3 = CalculateItem(index, _dividends[2], _moduli[2], _set3);

        return (item1, item2, item3);
    }

    public BigInteger IndexOf((T1, T2, T3) entry)
    {
        var index1 = _set1.IndexOf(entry.Item1);
        var index2 = _set2.IndexOf(entry.Item2);
        var index3 = _set3.IndexOf(entry.Item3);

        ValidateIndexOf(nameof(entry), index1, index2, index3);

        return index1 * _dividends[0] + index2 * _dividends[1] + index3 * _dividends[2];
    }

    public IEnumerable<(T1, T2, T3)> GenerateSamples(BigInteger sampleSize)
    {
        ValidateSampleSize(sampleSize, Size);

        return GenerateSamplesInternal(sampleSize);
    }

    private IEnumerable<(T1, T2, T3)> GenerateSamplesInternal(BigInteger sampleSize)
    {
        var sampledIndices = new HashSet<BigInteger>();

        while (sampledIndices.Count < (int)sampleSize)
        {
            var randomIndex = ThreadLocalRandom.NextBigInteger(0, Size);

            if (sampledIndices.Add(randomIndex))
            {
                yield return this[randomIndex];
            }
        }
    }

    private void Precompute()
    {
        Size *= _set1.Count;
        Size *= _set2.Count;
        Size *= _set3.Count;

        var factor = BigInteger.One;

        _dividends.Insert(0, factor);
        _moduli.Insert(0, new BigInteger(_set3.Count));

        factor *= _set3.Count;

        _dividends.Insert(0, factor);
        _moduli.Insert(0, new BigInteger(_set2.Count));

        factor *= _set2.Count;

        _dividends.Insert(0, factor);
        _moduli.Insert(0, new BigInteger(_set1.Count));
    }
}