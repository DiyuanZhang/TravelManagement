using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public abstract class ControllerFactBase : IDisposable
	{
		private TestServer _testServer;
		protected HttpClient HttpClient { get; set; }

		protected void Setup(Action<IServiceCollection> customAction = null)
		{
			_testServer = TestServerFactory.Create(services =>
			{
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