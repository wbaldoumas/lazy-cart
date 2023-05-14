using FluentAssertions;
using NUnit.Framework;
using System.Globalization;

namespace LazyCart.Tests;

[TestFixture]
public class LazyCartesianProductTests
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
    public void AtIndex_ShouldReturnCorrectTuple()
    {
        foreach (var index in Enumerable.Range(0, _set1!.Count * _set2!.Count).ToList())
        {
            _subjectUnderTest![index].Should().Be(_actualCartesianProduct![index]);
            _subjectUnderTest!.AtIndex(index).Should().Be(_actualCartesianProduct![index]);
        }
    }

    [Test]
    public void IndexOf_ShouldReturnCorrectIndex()
    {
        foreach (var (entry, index) in _cartesianProductIndices!)
        {
            _subjectUnderTest!.IndexOf(entry).Should().Be(index);
        }
    }
}