using TravelManagement.Domain.Models;

namespace TravelManagement.Domain.Repositories
{
	public interface IFlightOrderRepository
	{
		public FlightOrder Create(FlightOrder flightOrder);
	}
}