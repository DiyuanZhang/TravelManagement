using NHibernate;
using TravelManagement.Domain.Models;
using TravelManagement.Domain.Repositories;

namespace TravelManagement.Infrastructure.Repositories
{
	public class FlightOrderRepository : IFlightOrderRepository
	{
		private readonly ISession _session;

		public FlightOrderRepository(ISession session)
		{
			_session = session;
		}

		public FlightOrder Create(FlightOrder flightOrder)
		{
			_session.Save(flightOrder);
			_session.Flush();
			return flightOrder;
		}
	}
}