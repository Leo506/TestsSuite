using AutoFixture.NUnit3;
using Leo.TestsSuite.Core;

namespace Leo.TestsSuite.NUnit;

/// <summary>
///     This attribute uses for AutoFixture to generate values for unit test parameters.
///     Generator uses AutoMoqCustomization for creating values.
///     It allows to pass some predefined arguments to test like TestCaseAttribute from NUnit
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public partial class AutoMoqTestCaseAttribute : InlineAutoDataAttribute
{
    public object? ExpectedResult { get; set; }
    
    public AutoMoqTestCaseAttribute(params object?[]? arguments) : base(AutoMoqFixtureBuilder.CreateFixture, arguments)
    {
        TestMethodBuilder = new AutoMoqTestCaseMethodBuilder(this);
    }
}