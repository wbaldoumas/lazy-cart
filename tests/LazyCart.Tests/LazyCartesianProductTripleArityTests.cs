﻿using FluentAssertions;
using NUnit.Framework;
using System.Globalization;

namespace LazyCart.Tests;

[TestFixture]
public class LazyCartesianProductTripleArityTests
{
    private IList<int>? _set1;
    private IList<string>? _set2;
    private IList<double>? _set3;
    private IList<(int, string, double)>? _actualCartesianProduct;
    private IDictionary<(int, string, double), int>? _cartesianProductIndices;
    private ILazyCartesianProduct<int, string, double>? _subjectUnderTest;

    [SetUp]
    public void Setup()
    {
        _set1 = Enumerable.Range(1, 10).ToList();
        _set2 = Enumerable.Range(1, 10).Select(i => i.ToString(CultureInfo.CurrentCulture)).ToList();
        _set3 = Enumerable.Range(1, 10).Select(i => i * 1.0).ToList();
        _actualCartesianProduct = new List<(int, string, double)>();
        _cartesianProductIndices = new Dictionary<(int, string, double), int>();

        var index = 0;

        foreach (var item1 in _set1)
        {
            foreach (var item2 in _set2)
            {
                foreach (var item3 in _set3)
                {
                    _actualCartesianProduct.Add((item1, item2, item3));
                    _cartesianProductIndices.Add((item1, item2, item3), index++);
                }
            }
        }

        _subjectUnderTest = new LazyCartesianProduct<int, string, double>(_set1, _set2, _set3);
    }

    [Test]
    public void Size_ShouldReturnCorrectSize()
    {
        _subjectUnderTest!.Size.Should().Be(_set1!.Count * _set2!.Count * _set3!.Count);
    }

    [Test]
    public void AtIndex_ShouldReturnCorrectEntry()
    {
        foreach (var index in Enumerable.Range(0, _set1!.Count * _set2!.Count * _set3!.Count).ToList())
        {
            _subjectUnderTest![index].Should().Be(_actualCartesianProduct![index]);
            _subjectUnderTest!.AtIndex(index).Should().Be(_actualCartesianProduct![index]);
        }
    }

    [Test]
    [TestCase(100000000)]
    [TestCase(-1)]
    public void AtIndex_ShouldThrow_WhenIndexOutOfRange(int index)
    {
        var act = () => _subjectUnderTest!.AtIndex(index);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void IndexOf_ShouldReturnCorrectIndex()
    {
        foreach (var (entry, index) in _cartesianProductIndices!)
        {
            _subjectUnderTest!.IndexOf(entry).Should().Be(index);
        }
    }

    [Test]
    public void IndexOf_ShouldThrow_WhenEntryNotInProduct()
    {
        var act = () => _subjectUnderTest!.IndexOf((0, "0", 0.0));

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void GenerateSamples_ShouldReturnDistinctEntries()
    {
        var samples = _subjectUnderTest!.GenerateSamples(100).ToList();

        samples.Should().HaveCount(100);

        var distinctSamples = new HashSet<(int, string, double)>(samples);

        distinctSamples.Should().HaveCount(100);
    }

    [Test]
    public void GenerateSamples_ShouldThrow_WhenSampleSizeIsOutOfRange()
    {
        var act = () => _subjectUnderTest!.GenerateSamples(_subjectUnderTest.Size + 1).ToList();

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}