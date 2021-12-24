using Azure.Messaging.ServiceBus;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Context;
using TravelManagement.Application.Providers;
using TravelManagement.Infrastructure.Mappings;

namespace TravelManagement
{
	public static class StartupExtensions
    {
        public static void AddSession(this IServiceCollection services, string connectionString)
        {
            var fluentConfiguration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<FlightOrderRequestMapping>())
                .CurrentSessionContext<ThreadStaticSessionContext>();

            var sessionFactory = fluentConfiguration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(c =>
            {
                var session = sessionFactory.OpenSession();
                session.FlushMode = FlushMode.Commit;
                return session;
            });
        }

        public static void AddAzureServiceBus(this IServiceCollection services, string serviceBusConnectionString,
            string queueName)
        {
            var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
            var serviceBusSender = serviceBusClient.CreateSender(queueName);
            services.AddSingleton(serviceBusSender);
            services.AddTransient<IMessageSender, ServiceBusMessageSender>();
        }
    }
}