using AutoFixture.NUnit3;
using Leo.TestsSuite.Core;

namespace Leo.TestsSuite.NUnit;

/// <summary>
///     This attribute uses for AutoFixture to generate values for unit test parameters.
///     Generator uses AutoMoqCustomization for creating values
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AutoMoqDataAttribute() : AutoDataAttribute(AutoMoqFixtureBuilder.CreateFixture);