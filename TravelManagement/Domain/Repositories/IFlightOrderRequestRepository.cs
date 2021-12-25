using TravelManagement.Domain.Models;

namespace TravelManagement.Domain.Repositories
{
	public interface IFlightOrderRequestRepository
	{
		public FlightOrderRequest Create(FlightOrderRequest flightOrderRequest);
	}
}