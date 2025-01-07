using AutoFixture;
using AutoFixture.AutoMoq;

namespace Leo.TestsSuite.Core;

public static class AutoMoqFixtureBuilder
{
    public static IFixture CreateFixture()
    {
        var fixture = new Fixture()
            .Customize(new AutoMoqCustomization { ConfigureMembers = true, GenerateDelegates = true });
        
        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        return fixture;
    }
}