using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public class TestStartup : StartupBase
	{
		public TestStartup(IConfiguration configuration) : base(configuration)
		{
		}

		public void ConfigureServices(IServiceCollection services)
		{
			ConfigureCommonServices(services);
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			ConfigureCommon(app, env);
		}
	}
}