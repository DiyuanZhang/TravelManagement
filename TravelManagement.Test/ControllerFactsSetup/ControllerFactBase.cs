using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TravelManagement.Infrastructure.Utils;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public abstract class ControllerFactBase : IDisposable
	{
		private TestServer _testServer;
		protected HttpClient HttpClient { get; set; }

		protected void Setup(Action<IServiceCollection> customAction = null)
		{
			var mockTimeProvider = new Mock<ITimeProvider>();
			mockTimeProvider.Setup(p => p.UtcNow()).Returns(DateTime.Parse("2020-10-25"));
			_testServer = TestServerFactory.Create(services =>
			{
				services.AddScoped(_ => mockTimeProvider.Object);
				customAction?.Invoke(services);
			});
			_testServer.CreateScope();
			HttpClient = _testServer.CreateClient();
		}

		public void Dispose()
		{
			_testServer?.Dispose();
			HttpClient?.Dispose();
		}
	}
}