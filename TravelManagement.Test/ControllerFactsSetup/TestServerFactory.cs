using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public static class TestServerFactory
	{
		public static TestServer Create(Action<IServiceCollection> customize)
		{
			var webHostBuilder = new WebHostBuilder()
				.UseStartup<TestStartup>()
				.ConfigureTestServices(customize);
			var server = new Microsoft.AspNetCore.TestHost.TestServer(webHostBuilder);
			return new TestServer(server);
		}
	}
}