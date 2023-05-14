using FluentAssertions;
using NUnit.Framework;

namespace LazyCart.Tests;

[TestFixture]
public sealed class FooTests
{
    private Foo? _subjectUnderTest;

    [SetUp]
    public void SetUp() => _subjectUnderTest = new Foo();

    [Test]
    public void Bar_should_return_bar() => _subjectUnderTest!.Bar().Should().Be("Bar");
}