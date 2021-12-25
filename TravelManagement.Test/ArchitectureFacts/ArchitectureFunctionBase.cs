using System;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace TravelManagement.Test.ArchitectureFacts
{
	public class ArchitectureFunctionBase : IDisposable
	{
		protected ArchitectureFunctionBase(ITestOutputHelper outputHelper)
		{
			OutputHelper = outputHelper;
		}

		protected ITestOutputHelper OutputHelper { get; }

		protected TestResult Result { get; set; }
		protected string ApplicationNamespace { get; } = "TravelManagement.Application";
		protected string InterfaceNamespace { get; } = "TravelManagement.Interface";
		protected string DomainNamespace { get; } = "TravelManagement.Domain";
		protected string InfrastructureNamespace { get; } = "TravelManagement.Infrastructure";

		public void Dispose()
		{
			WriteFailuresIfFail();
		}

		private void WriteFailuresIfFail()
		{
			if (Result?.FailingTypes == null)
			{
				return;
			}

			foreach (var failingType in Result.FailingTypes)
			{
				OutputHelper.WriteLine(failingType.ToString());
			}
		}
	}
}