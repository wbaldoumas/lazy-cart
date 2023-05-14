using FluentAssertions;
using NUnit.Framework;
using System.Numerics;

namespace LazyCart.Tests;

[TestFixture]
public class ThreadLocalRandomTests
{
    [Test]
    public void Instance_ShouldReturnDifferentInstancesForDifferentThreads()
    {
        // arrange
        Random? instance1 = null;
        Random? instance2 = null;
        Random? instance3 = null;

        // act
        var thread1 = new Thread(() => instance1 = ThreadLocalRandom.Instance);
        var thread2 = new Thread(() => instance2 = ThreadLocalRandom.Instance);
        var thread3 = new Thread(() => instance3 = ThreadLocalRandom.Instance);

        thread1.Start();
        thread2.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        // assert
        instance1.Should().NotBeNull();
        instance2.Should().NotBeNull();
        instance3.Should().NotBeNull();

        instance1.Should().NotBeSameAs(instance2);
        instance1.Should().NotBeSameAs(instance3);
        instance2.Should().NotBeSameAs(instance3);
    }

    [Test]
    public void Instance_ShouldReturnSameInstanceForSameThread()
    {
        // arrange & act
        var instance1 = ThreadLocalRandom.Instance;
        var instance2 = ThreadLocalRandom.Instance;
        var instance3 = ThreadLocalRandom.Instance;

        // assert
        instance1.Should().BeSameAs(instance2);
        instance1.Should().BeSameAs(instance3);
        instance2.Should().BeSameAs(instance3);
    }

    [Test]
    public void NextBigInteger_ShouldReturnBigIntegerWithinRange()
    {
        // arrange
        var from = new BigInteger(0);
        var to = new BigInteger(long.MaxValue);

        // act
        var result = ThreadLocalRandom.NextBigInteger(from, to);

        // assert
        result.Should().BeGreaterOrEqualTo(from);
        result.Should().BeLessThan(to);
    }

    [Test]
    public void NextBigInteger_ShouldReturnDistinctValues()
    {
        // arrange
        var from = new BigInteger(0);
        var to = new BigInteger(long.MaxValue);

        // act
        var result1 = ThreadLocalRandom.NextBigInteger(from, to);
        var result2 = ThreadLocalRandom.NextBigInteger(from, to);
        var result3 = ThreadLocalRandom.NextBigInteger(from, to);

        // assert
        result1.Should().NotBe(result2);
        result1.Should().NotBe(result3);
        result2.Should().NotBe(result3);
    }
}