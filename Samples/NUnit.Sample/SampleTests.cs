using FluentAssertions;
using Leo.TestsSuite.NUnit;
using Moq;

namespace NUnit.Sample;

public class SampleTests
{
    [Test]
    [AutoMoqData]
    public void Test1(Mock<ISomeDependency> mockDependency)
    {
        mockDependency.Should().NotBeNull();
    }

    [Test]
    [AutoMoqTestCase("123")]
    public void Test2(string value, Mock<ISomeDependency> mockDependency)
    {
        value.Should().Be("123");
        mockDependency.Should().NotBeNull();
    }

    [Test]
    [AutoMoqTestCase("123", ExpectedResult = "123")]
    [AutoMoqTestCase("12345", ExpectedResult = "12345")]
    [AutoMoqTestCase("---", ExpectedResult = "---")]
    public string Test3(string value, Mock<ISomeDependency> _) => value;

    [Test]
    [AutoMoqTestCase(null, ExpectedResult = null)]
    public object Test4(object value, Mock<ISomeDependency> _) => value;

    public interface ISomeDependency;
}