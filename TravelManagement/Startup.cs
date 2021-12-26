using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TravelManagement.Application.Dtos;
using TravelManagement.Infrastructure.Utils;
using TravelManagement.Interface.Controllers;
using TravelManagement.Interface.Filters;

namespace TravelManagement
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);
            services.AddProviders(Configuration);
            services.AddApplicationService();
            services.AddDomainService();
            services.AddAzureServiceBus(Configuration["ServiceBusConnectionString"], Configuration["ServiceBusQueue"]);
            services.AddSession(Configuration["DBConnectionString"]);
            services.AddRepository();
            services.AddTransient<ITimeProvider, TimeProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureCommon(app, env);
        }
    }
    
    public class StartupBase
    {
        public StartupBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add<ServiceExceptionFilter>();
                    options.Filters.Add<AuthorizationFilter>();
                })
                .AddApplicationPart(typeof(FlightOrderController).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelManagement", Version = "v1" });
            });
            services.AddHealthChecks();
            services.AddScoped<CurrentUser>();
        }

        public void ConfigureCommon(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelManagement v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
