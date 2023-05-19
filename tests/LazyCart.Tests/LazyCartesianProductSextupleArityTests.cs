using FluentAssertions;
using NUnit.Framework;
using System.Globalization;

namespace LazyCart.Tests;

[TestFixture]
public class LazyCartesianProductSextupleArityTests
{
    private IList<int>? _set1;
    private IList<string>? _set2;
    private IList<double>? _set3;
    private IList<char>? _set4;
    private IList<bool>? _set5;
    private IList<float>? _set6;
    private IList<(int, string, double, char, bool, float)>? _actualCartesianProduct;
    private IDictionary<(int, string, double, char, bool, float), int>? _cartesianProductIndices;
    private ILazyCartesianProduct<int, string, double, char, bool, float>? _subjectUnderTest;

    [SetUp]
    public void Setup()
    {
        _set1 = Enumerable.Range(1, 10).ToList();
        _set2 = Enumerable.Range(1, 10).Select(i => i.ToString(CultureInfo.CurrentCulture)).ToList();
        _set3 = Enumerable.Range(1, 10).Select(i => i * 1.0).ToList();
        _set4 = Enumerable.Range(0, 10).Select(i => (char)('A' + i)).ToList();
        _set5 = new List<bool> { false, true };
        _set6 = Enumerable.Range(1, 10).Select(i => i * 1.0f).ToList();
        _actualCartesianProduct = new List<(int, string, double, char, bool, float)>();
        _cartesianProductIndices = new Dictionary<(int, string, double, char, bool, float), int>();

        var index = 0;

        foreach (var item1 in _set1)
        {
            foreach (var item2 in _set2)
            {
                foreach (var item3 in _set3)
                {
                    foreach (var item4 in _set4)
                    {
                        foreach (var item5 in _set5)
                        {
                            foreach (var item6 in _set6)
                            {
                                _actualCartesianProduct.Add((item1, item2, item3, item4, item5, item6));
                                _cartesianProductIndices.Add((item1, item2, item3, item4, item5, item6), index++);
                            }
                        }
                    }
                }
            }
        }

        _subjectUnderTest = new LazyCartesianProduct<int, string, double, char, bool, float>(
            _set1,
            _set2,
            _set3,
            _set4,
            _set5,
            _set6
        );
    }

    [Test]
    public void Size_ShouldReturnCorrectSize()
    {
        _subjectUnderTest!.Size
            .Should()
            .Be(_set1!.Count * _set2!.Count * _set3!.Count * _set4!.Count * _set5!.Count * _set6!.Count);
    }

    [Test]
    public void AtIndex_ShouldReturnCorrectEntry()
    {
        var maxIndex = _set1!.Count * _set2!.Count * _set3!.Count * _set4!.Count * _set5!.Count * _set6!.Count;

        foreach (var index in Enumerable.Range(0, maxIndex).ToList())
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
        var act = () => _subjectUnderTest!.IndexOf((0, "0", 0.0, '-', false, 0.0f));

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void GenerateSamples_ShouldReturnDistinctEntries()
    {
        var samples = _subjectUnderTest!.GenerateSamples(100).ToList();

        samples.Should().HaveCount(100);

        var distinctSamples = new HashSet<(int, string, double, char, bool, float)>(samples);

        distinctSamples.Should().HaveCount(100);
    }

    [Test]
    public void GenerateSamples_ShouldThrow_WhenSampleSizeIsOutOfRange()
    {
        var act = () => _subjectUnderTest!.GenerateSamples(_subjectUnderTest.Size + 1).ToList();

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}