## TestsSuite
This library is a collection of usefull libraries and some customizations for unit testing.  
### Libraries in use
+ [Moq](https://github.com/devlooped/moq) - library for mocking dependencies
+ [AutoFixture](https://github.com/AutoFixture/AutoFixture) + AutoMoq Customization - library for simplifying writing tests setups
+ [FluentAssetions](https://fluentassertions.com/) - library for writing clear asserts

### Customizations
#### `AutoMoqDataAttribute`
This attribute use [AutoMoq Customization package](https://www.nuget.org/packages/AutoFixture.AutoMoq) for creating tests parameters and automatically mocking it.  
Usage example:
```csharp
[Test]
[AutoMoqData]
public void Test1(Mock<ISomeDependency> mockDependency)
{
  mockDependency.Should().NotBeNull();
}
```

#### `AutoMoqTestCaseAttribute`
This attribute does the same as `AutoMoqDataAttribute`, but add `TestCaseAttribute` behavior. You can pass some predefiend parameters to it and then use them in tests.  
Usage example:
```csharp
[Test]
[AutoMoqTestCase("123")]
public void Test2(string value, Mock<ISomeDependency> mockDependency)
{
    value.Should().Be("123");
    mockDependency.Should().NotBeNull();
}
```
`AutoMoqTestCaseAttribute` also supports `ExpectedResult` property as `TestCaseAttribute`:
```csharp
[Test]
[AutoMoqTestCase("123", ExpectedResult = "123")]
[AutoMoqTestCase("12345", ExpectedResult = "12345")]
[AutoMoqTestCase("---", ExpectedResult = "---")]
public string Test3(string value, Mock<ISomeDependency> _) => value;
```

### Installation
Just add nuget package to your test project
```
dotnet add package Leo.TestsSuite
```
```csproj
<PackageReference Include="Leo.TestsSuite" Version="1.0.1" />
```
