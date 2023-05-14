﻿using FluentAssertions;
using FsCheck;
using System.Globalization;
using PropertyAttribute = FsCheck.Xunit.PropertyAttribute;

namespace LazyCart.Tests;

public sealed class LazyCartesianProductPropertyTests
{
    private readonly IList<int>? _set1;
    private readonly IList<string>? _set2;
    private readonly ILazyCartesianProduct<int, string>? _subjectUnderTest;

    public LazyCartesianProductPropertyTests()
    {
        _set1 = Enumerable.Range(1, 500000).ToList();
        _set2 = Enumerable.Range(1, 500000).Select(i => i.ToString(CultureInfo.CurrentCulture)).ToList();
        _subjectUnderTest = new LazyCartesianProduct<int, string>(_set1, _set2);
    }

    [Property(MaxTest = 100000)]
    public void AtIndex_ShouldReturnTuple_WhenIndexInRange(NonNegativeInt index)
    {
        if (index.Get < _set1!.Count * _set2!.Count)
        {
            var entry = _subjectUnderTest!.AtIndex(index.Get);

            _set1.Should().Contain(entry.Item1);
            _set2.Should().Contain(entry.Item2);

            entry = _subjectUnderTest[index.Get];

            _set1.Should().Contain(entry.Item1);
            _set2.Should().Contain(entry.Item2);
        }
        else
        {
            var act = () => _subjectUnderTest!.AtIndex(index.Get);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }

    [Property(MaxTest = 100000)]
    public void IndexOf_ShouldReturnIndex_WhenTupleInProduct(NonNegativeInt item1, NonNegativeInt item2)
    {
        var entry = (_set1![item1.Get % _set1.Count], _set2![item2.Get % _set2.Count]);

        var result = _subjectUnderTest!.IndexOf(entry);

        result.Should().BeInRange(0, _set1!.Count * _set2!.Count - 1);
    }

    [Property(MaxTest = 100000)]
    public void GenerateSamples_ShouldReturnDistinctValues(NonNegativeInt sampleSize)
    {
        if (sampleSize.Get < _set1!.Count * _set2!.Count)
        {
            var samples = _subjectUnderTest!.GenerateSamples(sampleSize.Get).ToList();

            samples.Should().HaveCount(sampleSize.Get);
            samples.Should().OnlyHaveUniqueItems();
        }
        else
        {
            var act = () => _subjectUnderTest!.GenerateSamples(sampleSize.Get);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}