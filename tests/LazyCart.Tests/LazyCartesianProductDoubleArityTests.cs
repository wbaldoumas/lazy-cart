using FluentAssertions;
using NUnit.Framework;
using System.Globalization;

namespace LazyCart.Tests;

[TestFixture]
public class LazyCartesianProductDoubleArityTests
{
    private IList<int>? _set1;
    private IList<string>? _set2;
    private IList<(int, string)>? _actualCartesianProduct;
    private IDictionary<(int, string), int>? _cartesianProductIndices;
    private ILazyCartesianProduct<int, string>? _subjectUnderTest;

    [SetUp]
    public void Setup()
    {
        _set1 = Enumerable.Range(1, 100).ToList();
        _set2 = Enumerable.Range(1, 100).Select(i => i.ToString(CultureInfo.CurrentCulture)).ToList();
        _actualCartesianProduct = new List<(int, string)>();
        _cartesianProductIndices = new Dictionary<(int, string), int>();

        var index = 0;

        foreach (var item1 in _set1)
        {
            foreach (var item2 in _set2)
            {
                _actualCartesianProduct.Add((item1, item2));
                _cartesianProductIndices.Add((item1, item2), index++);
            }
        }

        _subjectUnderTest = new LazyCartesianProduct<int, string>(_set1, _set2);
    }

    [Test]
    public void Size_ShouldReturnCorrectSize()
    {
        _subjectUnderTest!.Size.Should().Be(_set1!.Count * _set2!.Count);
    }

    [Test]
    public void AtIndex_ShouldReturnCorrectEntry()
    {
        foreach (var index in Enumerable.Range(0, _set1!.Count * _set2!.Count).ToList())
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
        var act = () => _subjectUnderTest!.IndexOf((0, "0"));

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void GenerateSamples_ShouldReturnDistinctEntries()
    {
        var samples = _subjectUnderTest!.GenerateSamples(100).ToList();

        samples.Should().HaveCount(100);

        var distinctSamples = new HashSet<(int, string)>(samples);

        distinctSamples.Should().HaveCount(100);
    }

    [Test]
    public void GenerateSamples_ShouldThrow_WhenSampleSizeIsOutOfRange()
    {
        var act = () => _subjectUnderTest!.GenerateSamples(_subjectUnderTest.Size + 1).ToList();

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}