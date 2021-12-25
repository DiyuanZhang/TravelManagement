using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public class TestServer : IDisposable
	{
		private readonly Microsoft.AspNetCore.TestHost.TestServer _testServer;

		public TestServer(Microsoft.AspNetCore.TestHost.TestServer testServer)
		{
			this._testServer = testServer;
		}

		public IServiceScope CreateScope()
		{
			return _testServer.Services.CreateScope();
		}

		public HttpClient CreateClient()
		{
			return _testServer.CreateClient();
		}

		public void Dispose()
		{
			_testServer?.Dispose();
		}
	}
}