using System.Reflection;
using AutoFixture.NUnit3;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace Leo.TestsSuite.NUnit;

public partial class AutoMoqTestCaseAttribute
{
    private sealed class AutoMoqTestCaseMethodBuilder(AutoMoqTestCaseAttribute attribute) : ITestMethodBuilder
    {
        /// <inheritdoc />
        public TestMethod Build(
            IMethodInfo method,
            Test suite,
            IEnumerable<object> parameterValues,
            int autoDataStartIndex)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));
            if (parameterValues == null) throw new ArgumentNullException(nameof(parameterValues));
            var parameters = GetParametersForMethod(method, parameterValues, autoDataStartIndex);
            SetExpectedResult(method, parameters);
            return new NUnitTestCaseBuilder().BuildTestMethod(method, suite, parameters);
        }

        private static TestCaseParameters GetParametersForMethod(
            IMethodInfo method,
            IEnumerable<object> parameterValues,
            int autoDataStartIndex)
        {
            try
            {
                return GetParametersForMethod(method, parameterValues.ToArray(), autoDataStartIndex);
            }
            catch (Exception ex)
            {
                return new TestCaseParameters(ex);
            }
        }

        private static TestCaseParameters GetParametersForMethod(
            IMethodInfo method,
            object[] args,
            int autoDataStartIndex)
        {
            var parameters1 = new TestCaseParameters(args);
            EnsureOriginalArgumentsArrayIsNotShared(parameters1);
            IParameterInfo[] parameters2 = method.GetParameters();
            for (var index = autoDataStartIndex; index < parameters1.OriginalArguments.Length; ++index)
                parameters1.OriginalArguments[index] = new TypeNameRenderer(parameters2[index].ParameterType);
            return parameters1;
        }

        /// <summary>
        ///     Before NUnit 3.5 the Arguments and OriginalArguments properties are referencing the same array, so
        ///     we cannot safely update the OriginalArguments without touching the Arguments value.
        ///     This method fixes that by making the OriginalArguments array a standalone copy.
        ///     <para>
        ///         When running in NUnit3.5 and later the method is supposed to do nothing.
        ///     </para>
        /// </summary>
        private static void EnsureOriginalArgumentsArrayIsNotShared(TestCaseParameters parameters)
        {
            if (parameters.Arguments != parameters.OriginalArguments)
                return;
            var destinationArray = new object[parameters.OriginalArguments.Length];
            Array.Copy(parameters.OriginalArguments, destinationArray, parameters.OriginalArguments.Length);
            typeof(TestParameters).GetTypeInfo().GetProperty("OriginalArguments")
                ?.SetValue(parameters, destinationArray, null);
        }
        
        private void SetExpectedResult(IMethodInfo method, TestCaseParameters parameters)
        {
            if (method.ReturnType.Type != typeof(void) && method.ReturnType.Type != typeof(Task))
                parameters.ExpectedResult = attribute.ExpectedResult;
        }

        private class TypeNameRenderer(Type type)
        {
            private Type Type { get; } = type ?? throw new ArgumentNullException(nameof(type));

            public override string ToString() => "auto<" + Type.Name + ">";
        }
    }
}