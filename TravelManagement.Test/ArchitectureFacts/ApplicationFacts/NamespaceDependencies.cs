using NetArchTest.Rules;
using Xunit;
using Xunit.Abstractions;

namespace TravelManagement.Test.ArchitectureFacts.ApplicationFacts
{
	public class NamespaceDependencies : ArchitectureFunctionBase
	{
		public NamespaceDependencies(ITestOutputHelper outputHelper)
			: base(outputHelper)
		{
		}
        
		[Fact]
		public void should_not_depend_on_interfaces_namespace()
		{
			Result = Types.InNamespace(ApplicationNamespace)
				.ShouldNot()
				.HaveDependencyOn(InterfaceNamespace)
				.GetResult();

			Assert.True(Result.IsSuccessful);
		}

		[Theory]
		[InlineData("System.Web")]
		[InlineData("System.Http")]
		public void should_not_depend_on_any_web_related_namespace(string webRelatedNamespace)
		{
			Result = Types.InNamespace(ApplicationNamespace)
				.ShouldNot()
				.HaveDependencyOn(webRelatedNamespace)
				.GetResult();

			Assert.True(Result.IsSuccessful);
		}

		[Fact]
		public void should_not_depend_on_fluent_nhibernate_namespace()
		{
			Result = Types.InNamespace(ApplicationNamespace)
				.ShouldNot()
				.HaveDependencyOn("FluentNHibernate")
				.GetResult();

			Assert.True(Result.IsSuccessful);
		}
	}
}