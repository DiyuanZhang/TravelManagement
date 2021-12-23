using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public class TestServer : IDisposable
	{
		private readonly Microsoft.AspNetCore.TestHost.TestServer testServer;

		public TestServer(Microsoft.AspNetCore.TestHost.TestServer testServer)
		{
			this.testServer = testServer;
		}

		public IServiceScope CreateScope()
		{
			return testServer.Services.CreateScope();
		}

		public HttpClient CreateClient()
		{
			return testServer.CreateClient();
		}

		public void Dispose()
		{
			testServer?.Dispose();
		}
	}
}