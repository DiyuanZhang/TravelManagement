using System.Linq;
using NHibernate;
using TravelManagement.Domain.Models;
using TravelManagement.Domain.Repositories;

namespace TravelManagement.Infrastructure.Repositories
{
	public class FlightOrderRequestRepository : IFlightOrderRequestRepository
	{
		private readonly ISession _session;

		public FlightOrderRequestRepository(ISession session)
		{
			_session = session;
		}

		public FlightOrderRequest Create(FlightOrderRequest flightOrderRequest)
		{
			_session.Save(flightOrderRequest);
			_session.Flush();
			return flightOrderRequest;
		}

		public FlightOrderRequest FindById(long id)
		{
			return _session.Query<FlightOrderRequest>().SingleOrDefault(o => o.Id == id);
		}
	}
}