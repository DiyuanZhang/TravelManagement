using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public abstract class ControllerFactBase : IDisposable
	{
		private TestServer testServer;
		protected IServiceProvider ServiceProvider;
		protected TestMocks TestMocks { get; set; }
		protected HttpClient HttpClient { get; set; }

		public ControllerFactBase(Action<IServiceCollection> customAction = null)
		{
			TestMocks = new TestMocks();
			testServer = TestServerFactory.Create(services =>
			{
				customAction?.Invoke(services);
			});
			ServiceProvider = testServer.CreateScope().ServiceProvider;
			HttpClient = testServer.CreateClient();
		}

		public void Dispose()
		{
			testServer?.Dispose();
			HttpClient?.Dispose();
		}
	}
}