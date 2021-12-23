using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public static class TestServerFactory
	{
		public static TestServer Create(Action<IServiceCollection> customize)
		{
			var webHostBuilder = new WebHostBuilder()
				.ConfigureAppConfiguration((c, b) =>
				{
					b.AddInMemoryCollection(new Dictionary<string, string>
					{
						{ "HelpCenterUrl", "survey.com" },
						{ "SurveyNotificationFromEmail", "testNoReply.com" },
						{ "SurveyReminderCount", "2" },
						{ "SurveyReminderIntervalDays", "10" }
					});
				})
				.UseStartup<TestStartup>()
				.ConfigureTestServices(customize);
			var server = new Microsoft.AspNetCore.TestHost.TestServer(webHostBuilder);
			return new TestServer(server);
		}
	}
}