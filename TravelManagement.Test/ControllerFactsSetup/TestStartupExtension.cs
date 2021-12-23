using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using TravelManagement.Infrastructure.Mappings;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Engine;
using NHibernate.Tool.hbm2ddl;

namespace TravelManagement.Test.ControllerFactsSetup
{
	public static class TestStartupExtension
	{
		public static void AddInmemorySession(this IServiceCollection services)
		{
			Configuration inMemDbConfiguration = null;

			var fluentConfiguration = Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.InMemory().ConnectionString("Data Source=:memory:;Version=3;New=True;DateTimeKind=Utc;"))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrderMapping>())
				.CurrentSessionContext<ThreadStaticSessionContext>()
				.ExposeConfiguration(c => inMemDbConfiguration = c);

			var sessionFactory = fluentConfiguration.BuildSessionFactory();

			var dbConnection = ((ISessionFactoryImplementor)sessionFactory)
				.ConnectionProvider
				.GetConnection();
			var schemaExport = new SchemaExport(inMemDbConfiguration);
			schemaExport.Execute(false, true, false, dbConnection, null);

			services.AddSingleton(sessionFactory);
			services.AddScoped(c =>
			{
				var session = sessionFactory.WithOptions().Connection(dbConnection).FlushMode(FlushMode.Commit)
					.OpenSession();
				session.FlushMode = FlushMode.Commit;
				return session;
			});
		}
	}
}