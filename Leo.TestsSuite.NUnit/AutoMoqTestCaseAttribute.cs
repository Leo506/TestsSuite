using AutoFixture.NUnit3;
using Leo.TestsSuite.Core;

namespace Leo.TestsSuite.NUnit;

/// <summary>
/// This attribute uses for AutoFixture to generate values for unit test parameters.
/// Generator uses AutoMoqCustomization for creating values.
/// It allows to pass some predefined arguments to test like TestCaseAttribute from NUnit
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AutoMoqTestCaseAttribute(params object?[]? arguments) 
    : InlineAutoDataAttribute(AutoMoqFixtureBuilder.CreateFixture, arguments);