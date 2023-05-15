using FluentAssertions;
using FsCheck;
using System.Globalization;
using PropertyAttribute = FsCheck.Xunit.PropertyAttribute;

namespace LazyCart.Tests;

public sealed class LazyCartesianProductQuintupleArityPropertyTests
{
    private readonly IList<int>? _set1;
    private readonly IList<string>? _set2;
    private readonly IList<double>? _set3;
    private readonly IList<char>? _set4;
    private readonly IList<bool>? _set5;
    private readonly ILazyCartesianProduct<int, string, double, char, bool>? _subjectUnderTest;

    public LazyCartesianProductQuintupleArityPropertyTests()
    {
        _set1 = Enumerable.Range(1, 100).ToList();
        _set2 = Enumerable.Range(1, 100).Select(i => i.ToString(CultureInfo.CurrentCulture)).ToList();
        _set3 = Enumerable.Range(1, 100).Select(i => (double)i).ToList();
        _set4 = Enumerable.Range(0, 26).Select(i => (char)('A' + i)).ToList();
        _set5 = new List<bool> { true, false };

        _subjectUnderTest = new LazyCartesianProduct<int, string, double, char, bool>(
            _set1,
            _set2,
            _set3,
            _set4,
            _set5
        );
    }

    [Property(MaxTest = 100000)]
    public void AtIndex_ShouldReturnEntry_WhenIndexInRange(NonNegativeInt index)
    {
        if (index.Get < _set1!.Count * _set2!.Count * _set3!.Count * _set4!.Count * _set5!.Count)
        {
            var entry = _subjectUnderTest!.AtIndex(index.Get);

            _set1.Should().Contain(entry.Item1);
            _set2.Should().Contain(entry.Item2);
            _set3.Should().Contain(entry.Item3);
            _set4.Should().Contain(entry.Item4);
            _set5.Should().Contain(entry.Item5);

            entry = _subjectUnderTest[index.Get];

            _set1.Should().Contain(entry.Item1);
            _set2.Should().Contain(entry.Item2);
            _set3.Should().Contain(entry.Item3);
            _set4.Should().Contain(entry.Item4);
            _set5.Should().Contain(entry.Item5);
        }
        else
        {
            var act = () => _subjectUnderTest!.AtIndex(index.Get);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }

    [Property(MaxTest = 100000)]
    public void IndexOf_ShouldReturnIndex_WhenEntryInProduct(
        NonNegativeInt item1,
        NonNegativeInt item2,
        NonNegativeInt item3,
        NonNegativeInt item4,
        NonNegativeInt item5)
    {
        var entry = (
            _set1![item1.Get % _set1.Count],
            _set2![item2.Get % _set2.Count],
            _set3![item3.Get % _set3.Count],
            _set4![item4.Get % _set4.Count],
            _set5![item5.Get % _set5.Count]
        );

        var result = _subjectUnderTest!.IndexOf(entry);

        result.Should().BeInRange(0, _set1!.Count * _set2!.Count * _set3!.Count * _set4!.Count * _set5!.Count - 1);
    }

    [Property(MaxTest = 100000)]
    public void GenerateSamples_ShouldReturnDistinctValues(NonNegativeInt sampleSize)
    {
        if (sampleSize.Get < _set1!.Count * _set2!.Count * _set3!.Count * _set4!.Count * _set5!.Count)
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