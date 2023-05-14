﻿using System.Numerics;
using System.Runtime.CompilerServices;

namespace LazyCart;

/// <inheritdoc cref="ILazyCartesianProduct{T1,T2}"/>
public sealed class LazyCartesianProduct<T1, T2> : ILazyCartesianProduct<T1, T2>
{
    private readonly IList<T1> _set1;
    private readonly IList<T2> _set2;
    private readonly IList<BigInteger> _dividends;
    private readonly IList<BigInteger> _moduli;
    private BigInteger _maxSize;

    public LazyCartesianProduct(IList<T1> set1, IList<T2> set2)
    {
        _set1 = set1;
        _set2 = set2;
        _dividends = new List<BigInteger>();
        _moduli = new List<BigInteger>();
        _maxSize = BigInteger.One;

        Precompute();
    }

    public (T1, T2) this[BigInteger index] => AtIndex(index);

    public (T1, T2) AtIndex(BigInteger index)
    {
        if (index < BigInteger.Zero || index >= _maxSize)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var item1 = CalculateItem(index, _dividends[0], _moduli[0], _set1);
        var item2 = CalculateItem(index, _dividends[1], _moduli[1], _set2);

        return (item1, item2);
    }

    public BigInteger IndexOf((T1, T2) entry)
    {
        var index1 = _set1.IndexOf(entry.Item1);
        var index2 = _set2.IndexOf(entry.Item2);

        if (index1 == -1 || index2 == -1)
        {
            throw new ArgumentOutOfRangeException(nameof(entry), "Could not find Cartesian product entry");
        }

        return index1 * _dividends[0] + index2 * _dividends[1];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T CalculateItem<T>(BigInteger index, BigInteger dividend, BigInteger modulus, IList<T> set)
    {
        dividend = index / dividend;
        modulus = dividend % modulus;

        return set[(int)modulus];
    }

    private void Precompute()
    {
        _maxSize *= _set1.Count;
        _maxSize *= _set2.Count;

        var factor = BigInteger.One;

        _dividends.Insert(0, factor);
        _moduli.Insert(0, new BigInteger(_set2.Count));

        factor *= _set2.Count;

        _dividends.Insert(0, factor);
        _moduli.Insert(0, new BigInteger(_set1.Count));
    }
}