using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace TravelManagement.Test.DomainServiceFactsSetup
{
	public class DomainServiceFactBase
	{
		protected IServiceProvider Provider { get; }

		protected DomainServiceFactBase()
		{
			var services = new ServiceCollection();
			services.AddDomainService();
			services.AddInmemorySession();
			services.AddRepository();
			var rootServiceProvider = services.BuildServiceProvider();
			var serviceScope = rootServiceProvider.CreateScope();
			Provider = serviceScope.ServiceProvider;
		}
		
		public IQueryable<T> DbQuery<T>() where T : class
		{
			var session = Provider.GetRequiredService<ISession>();
			return session.Query<T>();
		}
		
		public void DbInsert<T>(T model) where T : class
		{
			var session = Provider.GetRequiredService<ISession>();
			session.Save(model);
			session.Flush();
		}
	}
}